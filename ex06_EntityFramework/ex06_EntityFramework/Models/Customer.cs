using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex06_EntityFramework.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int AddressId { get; set; }
        public Address ShippingAddress { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
