using Gestion_BU.Entities;
using Gestion_BU.Interfaces;

namespace Gestion_BU.Repositories
{
    public class UniversiteRepository : IUniversiteRepository
    {

        private List<Universite> Universities = new List<Universite>()
        {
            new Universite()
            {
                Id = 1,
                Name = "Cambridge",
                Forfait = Forfait.Standard
            },
            new Universite()
            {
                Id = 2,
                Name = "Poudlard",
                Forfait = Forfait.Premium
            },
             new Universite()
            {
                Id = 3,
                Name = "University of Oxford",
                Forfait = Forfait.Premium
            }
        };

        public Universite GetById(int universityId)
        {
            return Universities.Single(x => x.Id == universityId);
        }

        public List<Universite> GetAll()
        {
            return Universities.ToList();
        }
    }
}
