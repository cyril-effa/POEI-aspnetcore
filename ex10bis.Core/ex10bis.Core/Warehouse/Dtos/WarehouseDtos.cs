namespace ex10bis.Core.Warehouse.Dtos
{
    // CREATE
    public record CreateWarehouseRequest(
        string Name,
        string Address,
        int PostalCode,
        List<Entities.Order> Orders);
    public record CreateWarehouseResponse(
        bool Success,
        string Response,
        Entities.Warehouse? Warehouse);

    // DELETE
    public record DeleteWarehouseRequest(
        int Id);
    public record DeleteWarehouseResponse(
        bool Success,
        string Response);

    // EDIT
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

    // READ
    public record ReadWarehouseRequest(
        int Id);
    public record ReadWarehouseResponse(
        bool Success,
        string Response,
        Entities.Warehouse? Warehouse);
}
