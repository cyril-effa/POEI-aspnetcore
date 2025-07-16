using Gestion_BU.Entities;
using Gestion_BU.Interfaces;

namespace Gestion_BU.Repositories
{
    public class EtudiantRepository : IEtudiantRepository
    {
        private static List<Etudiant> _etudiants = new List<Etudiant>()
        {
            new Etudiant("quentin.martinez@cambridge.com", 1, true)
            {
                NbTelechargementMaximum = 10,
                NbLivreTelecharges = 5,
            },
            new Etudiant("john.wick@oxford.com", 3, false)
            {
                NbTelechargementMaximum = 5,
                NbLivreTelecharges = 2,
            },
            new Etudiant("harry.potter@poudlard.com", 2, true)
            {
                NbTelechargementMaximum = 5,
                NbLivreTelecharges = 0,
            }
        };

        public void Add(Etudiant student)
        {
            _etudiants.Add(student);
        }
        public bool Exists(string adresseEmail)
        {
           return _etudiants.Any(etudiant => etudiant.AdresseEmail == adresseEmail);
        }

        public List<Etudiant> GetEtudiants()
        {
            return _etudiants.ToList();
        }
    }
}
