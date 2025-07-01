using ex10_Final.Data;
using ex10_Final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ex10_Final.Controllers
{
    [Authorize(Roles = "livreur,magasinier")]
    public class DeliveryAssignmentController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeliveryAssignmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DeliveryAssignment
        public async Task<IActionResult> Index(string selectedLivreurId = null)
        {
            // Récupérer tous les livreurs
            var livreurs = await _userManager.GetUsersInRoleAsync("livreur");
            var livreurList = livreurs
                .Select(l => new SelectListItem
                {
                    Value = l.Id,
                    Text = l.UserName // ou l.Name si tu as une propriété Name
                })
                .ToList();

            // Pré-sélectionner le livreur connecté si c'est un livreur
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.IsInRole("livreur"))
            {
                selectedLivreurId ??= currentUser.Id;
            }

            ViewBag.Livreurs = livreurList;
            ViewBag.SelectedLivreurId = selectedLivreurId;

            // Filtrer les livraisons
            var assignments = _context.DeliveryAssignment.AsQueryable();

            if (!string.IsNullOrEmpty(selectedLivreurId))
            {
                assignments = assignments.Where(a => a.LivreurId == selectedLivreurId);
            }

            var assignmentList = await assignments.ToListAsync();

            // Récupérer les statuts des commandes associées
            var orderIds = assignmentList.Select(a => a.OrderId).Distinct().ToList();
            var orderStatuses = await _context.Order
                .Where(o => orderIds.Contains(o.Id))
                .ToDictionaryAsync(o => o.Id, o => o.OrderStatus);

            ViewBag.OrderStatuses = orderStatuses;

            return View(await assignments.ToListAsync());
        }

        public async Task<IActionResult> ConfirmerLivraison(int id)
        {
            var deliveryAssignment = await _context.DeliveryAssignment.FindAsync(id);
            if (deliveryAssignment == null)
            {
                return NotFound();
            }

            if (deliveryAssignment.LivreurId != _userManager.GetUserId(User))
            {
                return Forbid(); // L'utilisateur n'est pas autorisé à confirmer cette livraison
            }

            // Modifier le status de la commande 
            var order = await _context.Order
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Article)
                .FirstOrDefaultAsync(o => o.Id == deliveryAssignment.OrderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Delivered;
                _context.Order.Update(order);

                // Génération de la facture PDF
                var facture = new Facture
                {
                    NumeroFacture = $"{DateTime.Now:yyyy-MM}-{order.Id}",
                    OrderId = order.Id,
                    Date = DateTime.Now
                };

                // Génère le PDF (exemple avec QuestPDF)
                var factureFileName = $"Facture_{facture.NumeroFacture}.pdf";
                var facturePath = Path.Combine("wwwroot", "factures", factureFileName);

                Directory.CreateDirectory(Path.GetDirectoryName(facturePath));

                GlobalFontSettings.FontResolver = new FontResolver();

                var document = new PdfDocument();
                var page = document.AddPage();

                var gfx = XGraphics.FromPdfPage(page);
                var font = new XFont("Arial", 20, XFontStyle.Bold);

                var textColor = XBrushes.Black;
                var layout = new XRect(20, 20, page.Width, page.Height);
                var format = XStringFormats.TopLeft;
                
                gfx.DrawString($"Facture n° {facture.NumeroFacture}", font, textColor, new XRect(20, 40, page.Width, page.Height), format);
                gfx.DrawString($"Date : {facture.Date:dd/MM/yyyy}", font, textColor, new XRect(20, 60, page.Width, page.Height), format);
                gfx.DrawString($"Commande n° {order.Id}", font, textColor, new XRect(20, 80, page.Width, page.Height), format);
                gfx.DrawString($"Client : {order.CustomerId}", font, textColor, new XRect(20, 100, page.Width, page.Height), format);
                gfx.DrawString($"Articles :", font, textColor, new XRect(20, 120, page.Width, page.Height), format);

                var y = 140; // Position de départ pour les articles
                foreach (var detail in order.OrderDetails)
                {
                    gfx.DrawString($"   - {detail.Article.Name} ({detail.UnitPrice:C2}) x {detail.Quantity} = {(detail.Quantity * detail.UnitPrice):C2}", font, textColor, new XRect(20, y, page.Width, page.Height), format);
                    y += 20; // Espace entre les lignes
                }

                gfx.DrawString($"Coût de livraison : {order.ShippingCost:C2}", font, textColor, new XRect(20, y, page.Width, page.Height), format);
                gfx.DrawString($"Montant total : {order.TotalAmount:C2}", font, textColor, new XRect(20, y + 20, page.Width, page.Height), format);
                gfx.DrawString($"Merci pour votre commande !", font, textColor, new XRect(20, y + 40, page.Width, page.Height), format);

                document.Save(facturePath);

                facture.FilePath = "/factures/" + factureFileName;
                _context.Add(facture);

                // Lier la facture à la commande
                order.FactureId = facture.Id;
                await _context.SaveChangesAsync();

                // Envoi du mail avec pièce jointe
                var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == order.CustomerId);
                if (customer != null)
                {
                    var emailSender = HttpContext.RequestServices.GetService(typeof(IEmailSender)) as IEmailSender;
                    if (emailSender != null)
                    {
                        byte[] pdfBytes;
                        using (var ms = new MemoryStream())
                        {
                            document.Save(ms, false);
                            pdfBytes = ms.ToArray();
                        }

                        var subject = "Votre facture de livraison";
                        var body = $"Bonjour {customer.Name},<br/>Veuillez trouver en pièce jointe la facture de votre commande n°{order.Id}.";
                        // Utilisation d'un service d'email personnalisé avec pièce jointe

                        var to = "cyril.rastel@fortil.group"; //customer.Email;
                        //await SendMailAsync(emailSender, customer.Email, subject, body, pdfBytes, factureFileName);
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AnnulerLivraison(int id)
        {
            var deliveryAssignment = await _context.DeliveryAssignment.FindAsync(id);
            if (deliveryAssignment == null)
            {
                return NotFound();
            }
            if (deliveryAssignment.LivreurId != _userManager.GetUserId(User))
            {
                return Forbid(); // L'utilisateur n'est pas autorisé à annuler cette livraison
            }

            // Modifier le status de la commande 
            var order = await _context.Order.FindAsync(deliveryAssignment.OrderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Cancelled;
                _context.Order.Update(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public static async Task SendMailAsync(IEmailSender sender, string to, string subject, string htmlBody, byte[] attachment, string attachmentName)
        {
            // Si tu utilises un service SMTP custom, adapte ici.
            // Exemple avec SmtpClient (à adapter selon ta config) :
            using var message = new MailMessage();
            message.From = new MailAddress("cyril.rastel@gmail.com");
            //message.To.Add(to);
            message.To.Add("cyril.rastel@fortil.group"); // TEST
            message.Subject = subject;
            message.Body = htmlBody;
            message.IsBodyHtml = true;
            message.Attachments.Add(new Attachment(new MemoryStream(attachment), attachmentName, MediaTypeNames.Application.Pdf));

            using var smtp = new SmtpClient("smtp.freesmtpservers.com", 25); // configure ton serveur SMTP
            await smtp.SendMailAsync(message);
        }
    }
}
