#nullable disable
using Ex_08.Controllers;
using Ex_08.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TPBiblio.Models.BO;

namespace TPBiblio.Controllers
{
    public class LivresController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LivresController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Livres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livres.ToListAsync());
        }

        // GET: Livres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .FirstOrDefaultAsync(m => m.LivreId == id);

            if (livre == null)
            {
                return NotFound();
            }

            if (livre.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value &&
                User.IsInRole("membre") && livre.Status != LivreStatus.Approved)
            {
                return Forbid(); // Unauthorized access
            }

            return View(livre);
        }

        // GET: Livres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivreId,Content,CreatedDate,Status,UserId")] Livre livre)
        {
            if (ModelState.IsValid)
            {
                livre.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _context.Add(livre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livre);
        }

        // GET: Livres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres.FindAsync(id);

            if (livre == null)
            {
                return NotFound();
            }

            if (livre.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid(); // Unauthorized access
            }

            return View(livre);
        }

        // POST: Livres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivreId,Content,CreatedDate,Status,UserId")] Livre livre)
        {
            if (id != livre.LivreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreExists(livre.LivreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livre);
        }

        // GET: Livres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .FirstOrDefaultAsync(m => m.LivreId == id);

            if (livre == null)
            {
                return NotFound();
            }

            if (livre.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value && !User.IsInRole("administrator"))
            {
                return Forbid(); // Unauthorized access
            }

            return View(livre);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "bibliothecaire")]
        public async Task<IActionResult> ApproveOrReject(int id, LivreStatus status)
        {
            var livre = await _context.Livres.FirstOrDefaultAsync(
                                          m => m.LivreId == id);

            if (livre == null)
            {
                return NotFound();
            }

            livre.Status = status;
            _context.Livres.Update(livre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivreExists(int id)
        {
            return _context.Livres.Any(e => e.LivreId == id);
        }
    }
}
