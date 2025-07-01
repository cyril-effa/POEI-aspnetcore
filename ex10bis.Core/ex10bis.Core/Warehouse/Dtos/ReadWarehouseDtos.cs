namespace ex10bis.Core.Warehouse.Dtos
{
    public record ReadWarehouseRequest(
        int Id);
    public record ReadWarehouseResponse(
        bool Success,
        string Response,
        Entities.Warehouse? Warehouse);
}
