using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BO;

namespace Exercice_5_MVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string ShippingAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le montant total doit être supérieur ou égal à 0.")]
        public double TotalAmount { get; set; }

        [Required]
        [ValidOrderStatus(ErrorMessage = "Le statut de la commande doit être valide.")]
        public string OrderStatus { get; set; }

        // Pour afficher les statuts disponibles dans un menu déroulant
        public List<string> AvailableStatuses { get; set; } = new List<string>
        {
            "EnAttente", "EnCours", "Expédié", "Livré", "Annulé"
        };

        [MinElements(1, ErrorMessage = "La commande doit contenir au moins un article.")]
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public int WarehouseId { get; set; }
    }
}