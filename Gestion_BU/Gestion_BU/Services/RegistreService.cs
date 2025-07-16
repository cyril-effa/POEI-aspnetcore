using Gestion_BU.Entities;
using Gestion_BU.Interfaces;
using Gestion_BU.Repositories;
using System.Runtime.CompilerServices;

namespace Gestion_BU.Services
{
    public class RegistreService : IRegistreService
    {
        public bool AjouterEtudiant(string emailAddress, int universityId, bool hasBonus)
        {
           
            Console.WriteLine(string.Format("Log: Debut Ajout d'un etudiant avec cet e-mail '{0}'", emailAddress));

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            var etudiantRepository = new EtudiantRepository();

            if (etudiantRepository.Exists(emailAddress))
            {
                return false;
            }

            var UniversiteRepository = new UniversiteRepository();

            var universite = UniversiteRepository.GetById(universityId);

            var currEtudiant = new Etudiant(emailAddress, universityId, hasBonus);


            currEtudiant.NbTelechargementMaximum = GestionForfaitMax(currEtudiant, universite);

            etudiantRepository.Add(currEtudiant);

            
            Console.WriteLine(string.Format("Log: Fin Ajout d'un etudiant avec cet e-mail '{0}'", emailAddress));

            return true;
        }

        private int GestionForfaitMax(Etudiant etudiant, Universite universite)
        {
            switch (universite.Forfait)
            {
                case Forfait.Standard:
                    return etudiant.HasBonus ? 15 : 10;
                case Forfait.Premium:
                    return etudiant.HasBonus ? 30 : 20;
                case Forfait.Illimité:
                    return -1;
                default:
                    throw new ArgumentException("Forfait inconnu");
            }
        }
    }
}
