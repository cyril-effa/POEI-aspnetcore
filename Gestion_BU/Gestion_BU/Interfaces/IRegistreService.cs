namespace Gestion_BU.Interfaces
{
    public interface IRegistreService
    {
        bool AjouterEtudiant(string emailAddress, int universityId, bool hasBonus);
    }
}
