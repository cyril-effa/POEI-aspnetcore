using ex10bis.Core.Warehouse.Dtos;
using ex10bis.Core.Warehouse.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ex10bis.Web.Controllers
{
    public class WarehouseController(IWarehouseRepository warehouseRepository, ICreateWarehouseUseCase createWarehouseUseCase, IEditWarehouseUseCase editWarehouseUseCase, IDeleteWarehouseUseCase deleteWarehouseUseCase, IReadWarehouseUseCase readWarehouseUseCase) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var warehouses = await warehouseRepository.ListAsync();
            return View(warehouses);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var response = await readWarehouseUseCase.Execute(new ReadWarehouseRequest(id.Value));
            if (!response.Success) return NotFound();
            return View(response.Warehouse);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWarehouseRequest request)
        {
            var response = await createWarehouseUseCase.Execute(request);
            if (response.Success)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", response.Response);
            return View(request);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var response = await readWarehouseUseCase.Execute(new ReadWarehouseRequest(id.Value));
            if (!response.Success) return NotFound();
            var editRequest = new EditWarehouseRequest(
                response.Warehouse.Id,
                response.Warehouse.Name,
                response.Warehouse.Address,
                response.Warehouse.PostalCode,
                response.Warehouse.Orders);
            return View(editRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditWarehouseRequest request)
        {
            var response = await editWarehouseUseCase.Execute(request);
            if (response.Success)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", response.Response);
            return View(request);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var response = await readWarehouseUseCase.Execute(new ReadWarehouseRequest(id.Value));
            if (!response.Success) return NotFound();
            return View(response.Warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteWarehouseRequest request)
        {
            var response = await deleteWarehouseUseCase.Execute(request);
            if (response.Success)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", response.Response);
            return View(request);
        }
    }
}