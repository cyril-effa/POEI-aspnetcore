using Gestion_BU.Entities;

namespace Gestion_BU.Interfaces
{
    public interface IEtudiantRepository
    {
        void Add(Etudiant student);
        bool Exists(string adresseEmail);
        List<Etudiant> GetEtudiants();
    }
}
