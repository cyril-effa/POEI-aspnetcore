using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex10_Final.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key to Identity user
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
