using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Interfaces
{
    public interface IFactureService
    {
        void GenerateFacture(string filePath, Facture facture, Entities.Order order);
        void SendFactureByEmail(string email, string subject, string body, byte[] attachment, string attachmentName);
    }
}
