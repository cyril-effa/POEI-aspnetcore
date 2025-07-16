using Gestion_BU.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gestion_BU.ViewModels
{
    public class UniversiteViewModel
    {
        public UniversiteViewModel()
        {

        }
        public int Id { get; set; }
        public string Nom { get; set; }

        public string NomForfait { get; set; }
    }
}
