namespace ex10bis.Core.Warehouse.Dtos
{
    public record EditWarehouseRequest(
        int Id,
        string Name,
        string Address,
        int PostalCode,
        List<Entities.Order> Orders);
    public record EditWarehouseResponse(
        bool Success,
        string Response,
        Entities.Warehouse? Warehouse);
}
