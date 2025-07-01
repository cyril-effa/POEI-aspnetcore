using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex07_WarehouseAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public int WarehouseId { get; internal set; }


        public double TotalAmount => OrderDetails?.Sum(od => (double)(od.Quantity * od.UnitPrice)) ?? 0;
    }
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public int WarehouseId { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }

}
