namespace ex10bis.Core.Warehouse.Dtos
{
    public record DeleteWarehouseRequest(
        int Id);
    public record DeleteWarehouseResponse(
        bool Success,
        string Response);
}
