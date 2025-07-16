using ex10bis.Core.Warehouse.Dtos;
using ex10bis.Core.Warehouse.Interfaces;

namespace ex10bis.Core.Warehouse.UseCases
{
    public class WarehouseUseCase(IWarehouseRepository warehouseRepository) : IWarehouseUseCase
    {
        public async Task<CreateWarehouseResponse> Create(CreateWarehouseRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Address) ||
                request.PostalCode <= 0)
            {
                return new CreateWarehouseResponse(false, "Invalid input data", null);
            }
            var warehouse = new Entities.Warehouse
            {
                Name = request.Name,
                Address = request.Address,
                PostalCode = request.PostalCode,
                Orders = request.Orders ?? new List<Entities.Order>()
            };

            try { 
                await warehouseRepository.AddAsync(warehouse);
                return new CreateWarehouseResponse(true, "Warehouse created successfully", warehouse);
            }
            catch (Exception ex)
            {
                return new CreateWarehouseResponse(false, $"Error creating warehouse: {ex.Message}", null);
            }

        }

        public Task<DeleteWarehouseResponse> Delete(DeleteWarehouseRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new DeleteWarehouseResponse(false, "Invalid request"));
            }

            var warehouse = warehouseRepository.GetByIdAsync(request.Id).Result;
            if (warehouse == null)
            {
                return Task.FromResult(new DeleteWarehouseResponse(false, "Warehouse not found"));
            }

            try
            {
                warehouseRepository.DeleteAsync(warehouse);
                return Task.FromResult(new DeleteWarehouseResponse(true, "Warehouse deleted successfully"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new DeleteWarehouseResponse(false, $"Error deleting warehouse: {ex.Message}"));
            }
        }

        public Task<EditWarehouseResponse> Edit(EditWarehouseRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new EditWarehouseResponse(false, "Invalid request", null));
            }

            var warehouse = warehouseRepository.GetByIdAsync(request.Id).Result;
            if (warehouse == null)
            {
                return Task.FromResult(new EditWarehouseResponse(false, "Warehouse not found", null));
            }

            warehouse.Name = request.Name;
            warehouse.Address = request.Address;
            warehouse.PostalCode = request.PostalCode;
            warehouse.Orders = request.Orders ?? new List<Entities.Order>();

            if (string.IsNullOrWhiteSpace(warehouse.Name) ||
                string.IsNullOrWhiteSpace(warehouse.Address) ||
                warehouse.PostalCode <= 0)
            {
                return Task.FromResult(new EditWarehouseResponse(false, "Invalid input data", null));
            }

            try
            { 
                warehouseRepository.UpdateAsync(warehouse);
                return Task.FromResult(new EditWarehouseResponse(true, "Warehouse updated successfully", warehouse));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new EditWarehouseResponse(false, $"Error updating warehouse: {ex.Message}", null));
            }
        }

        public Task<ReadWarehouseResponse> Read(ReadWarehouseRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new ReadWarehouseResponse(false, "Invalid request", null));
            }

            var warehouse = warehouseRepository.GetByIdAsync(request.Id).Result;
            if (warehouse == null)
            {
                return Task.FromResult(new ReadWarehouseResponse(false, "Warehouse not found", null));
            }
            return Task.FromResult(new ReadWarehouseResponse(true, "Warehouse retrieved successfully", warehouse));
        }
    }
}
