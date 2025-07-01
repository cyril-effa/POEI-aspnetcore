using BO;
using Exercice_5_MVC.Models;

namespace Exercice_5_MVC
{
    public class Mapping
    {
       public static WarehouseVM  ConvertToWarehouseVM(Warehouse warehouse)
        {
            var vm = new WarehouseVM();
            vm.Id = warehouse.Id;
            vm.Address = warehouse.Address;
            vm.Name = warehouse.Name;
            vm.PostalCode = warehouse.PostalCode;
            return vm;
        }

        public static Warehouse ToWarehouse(WarehouseVM wm)
        {
            var warehouse = new Warehouse();
            warehouse.Id = wm.Id;
            warehouse.Address = wm.Address;
            warehouse.Name = wm.Name;
            warehouse.PostalCode = wm.PostalCode;

            return warehouse;
        }

        // Order -> OrderViewModel
        public static OrderViewModel ToOrderViewModel(Order order)
        {
            if (order == null) return null;

            return new OrderViewModel
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Email = order.Email,
                ShippingAddress = order.ShippingAddress,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                OrderDetails = order.OrderDetails != null
                    ? new List<OrderDetail>(order.OrderDetails)
                    : new List<OrderDetail>(),
                WarehouseId = order.WarehouseId
            };
        }

        // OrderViewModel -> Order
        public static Order ToOrder(OrderViewModel vm)
        {
            if (vm == null) return null;

            return new Order
            {
                Id = vm.Id,
                CustomerName = vm.CustomerName,
                Email = vm.Email,
                ShippingAddress = vm.ShippingAddress,
                OrderDate = vm.OrderDate,
                TotalAmount = vm.TotalAmount,
                OrderStatus = vm.OrderStatus,
                OrderDetails = vm.OrderDetails != null
                    ? new List<OrderDetail>(vm.OrderDetails)
                    : new List<OrderDetail>(),
                WarehouseId = vm.WarehouseId
            };
        }
    }
}
