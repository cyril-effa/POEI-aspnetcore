using Gestion_BU.Interfaces;
using Gestion_BU.Repositories;
using Gestion_BU.Services;
using Gestion_BU.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_BU.Controllers
{
    public class EtudiantController (IRegistreService _registreService, IUniversiteRepository _universiteRepository, IEtudiantRepository _etudiantRepository) : Controller
    {
        public IActionResult Index()
        {
            var etudiants = _etudiantRepository.GetEtudiants();
            var etudiantsVm = new List<EtudiantViewModel>();
            foreach (var item in etudiants)
            {
                etudiantsVm.Add(new EtudiantViewModel()
                {
                    Email = item.AdresseEmail,
                    UniversiteId = item.UniversiteId,
                    UniversiteName = _universiteRepository.GetById(item.UniversiteId).Name ?? "Inconnu",
                    HasBonus = item.HasBonus,
                    NbTelechargements = $"{item.NbLivreTelecharges}/{item.NbTelechargementMaximum}"
                });
            }
            return View(etudiantsVm);
        }


        public IActionResult Add()
        {
            var vm = new EtudiantViewModel(_universiteRepository.GetAll());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(EtudiantViewModel viewModel)
        {
            _registreService.AjouterEtudiant(viewModel.Email, viewModel.UniversiteId, viewModel.HasBonus);
            return RedirectToAction("Index");
        }
    }
}
