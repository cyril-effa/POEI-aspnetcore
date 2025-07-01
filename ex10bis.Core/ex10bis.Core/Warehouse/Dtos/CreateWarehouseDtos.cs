namespace ex10bis.Core.Warehouse.Dtos
{
    public record CreateWarehouseRequest(
        string Name,
        string Address,
        int PostalCode,
        List<Entities.Order> Orders);
    public record CreateWarehouseResponse(
        bool Success,
        string Response,
        Entities.Warehouse? Warehouse);
}
