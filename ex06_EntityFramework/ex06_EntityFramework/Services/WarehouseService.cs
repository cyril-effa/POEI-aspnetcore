using ex06_EntityFramework.Interfaces;
using ex06_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex06_EntityFramework.Services
{
    internal class WarehouseService : IWarehouseService
    {
        public List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

        public List<Warehouse> GetAllWarehouses()
        {
            return Warehouses;
        }
    }
}
