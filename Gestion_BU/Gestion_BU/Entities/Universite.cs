using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gestion_BU.Entities
{
    public class Universite
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public Forfait Forfait { get;  set; }
    }
}