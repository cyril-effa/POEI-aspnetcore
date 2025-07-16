using Gestion_BU.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Gestion_BU.ViewModels
{
    public class EtudiantViewModel
    {
        public EtudiantViewModel()
        {
        }

        public EtudiantViewModel(List<Universite> universities)
        {
            Universites = new SelectList(universities, "Id", "Name");
        }

        public string? Email { get; set; }
        public int UniversiteId { get; set; }

        [DisplayName("Université")]
        public string UniversiteName { get; set; }

        public SelectList Universites { get; set; }
        public bool HasBonus { get; set; }
        public string NbTelechargements { get; set; }
    }
}
