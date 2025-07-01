using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01_Garage
{
    internal interface IOption
    {
        public double Prix { get; set; }

        public string ToString();
    }
}
