using Gestion_BU.Interfaces;
using Gestion_BU.Repositories;
using Gestion_BU.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_BU.Controllers
{
    public class UniversiteController(IUniversiteRepository _universiteRepository) : Controller
    {
        public IActionResult Index()
        {
            var universites = _universiteRepository.GetAll();
            var universitesVm = new List<UniversiteViewModel>();
            foreach (var item in universites)
            {
                universitesVm.Add(new UniversiteViewModel()
                {
                    Id = item.Id,
                    Nom = item.Name,
                    NomForfait = item.Forfait.ToString()
                });
            }
            return View(universitesVm);
        }


    }
}
