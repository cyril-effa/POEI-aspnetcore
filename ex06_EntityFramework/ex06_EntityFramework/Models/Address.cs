using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex06_EntityFramework.Models
{
    public class Address : ValueObject
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
        }
    }
}
