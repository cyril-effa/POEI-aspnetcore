using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex10_Final.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public int WarehouseId { get; set; }
        public int DureeLivraison { get; set; }
        public double ShippingCost { get; set; }
        public int FactureId { get; set; }


        public double TotalAmount => OrderDetails?.Sum(od => (double)(od.Quantity * od.UnitPrice)) + ShippingCost ?? 0;
    }

    public enum OrderStatus
    {
        Submitted,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
