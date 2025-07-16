using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gestion_BU.Entities
{
    public class Etudiant
    {
        public string AdresseEmail { get; private set; }
        public int UniversiteId { get; private set; }
        public int NbTelechargementMaximum { get; set; }
        public int NbLivreTelecharges { get; set; }
        public bool HasBonus { get; set; }

        public Etudiant(string emailAddress, int universityId, bool hasBonus)
        {
            AdresseEmail = emailAddress;
            UniversiteId = universityId;
            HasBonus = hasBonus;
        }
    }
}
