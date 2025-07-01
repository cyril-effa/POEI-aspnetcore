using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex06_EntityFramework.Models
{
    public class Order
    {
        public int Id { get; set; }


        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public double TotalAmount { get; set; }

        public string OrderStatus { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
       
        public int WarehouseId { get; internal set; }
        public Warehouse Warehouse { get;  set; }
    }
}
