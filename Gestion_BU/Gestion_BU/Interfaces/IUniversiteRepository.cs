using Gestion_BU.Entities;

namespace Gestion_BU.Interfaces
{
    public interface IUniversiteRepository
    {
        Universite GetById(int universityId);

        List<Universite> GetAll();
    }
}
