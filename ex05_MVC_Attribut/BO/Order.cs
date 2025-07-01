using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
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
        
        [MinElements(1, ErrorMessage = "La commande doit contenir au moins un article.")]
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public int WarehouseId { get; set; }
    }
}
