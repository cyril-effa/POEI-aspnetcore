using ex10bis.Core.Article.Dtos;
using ex10bis.Core.Article.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ex10bis.Web.Controllers
{
    public class ArticleController(IArticleRepository articleRepository, ICreateArticleUseCase createArticleUseCase, IEditArticleUseCase editArticleUseCase, IDeleteArticleUseCase deleteArticleUseCase, IReadArticleUseCase readArticleUseCase) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var articles = await articleRepository.ListAsync();
            return View(articles);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var response = await readArticleUseCase.Execute(new ReadArticleRequest(id.Value));
            if (!response.Success) return NotFound();
            return View(response.Article);
        }

        [Authorize(Roles = "magasinier")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateArticleRequest request)
        {
            var response = await createArticleUseCase.Execute(request);
            if (response.Success)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", response.Response);
            return View(request);
        }

        [Authorize(Roles = "magasinier")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var response = await readArticleUseCase.Execute(new ReadArticleRequest(id.Value));
            if (!response.Success) return NotFound();
            var editRequest = new EditArticleRequest(
                response.Article.Id,
                response.Article.Name,
                response.Article.Description,
                response.Article.Price,
                response.Article.StockQuantity);
            return View(editRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditArticleRequest request)
        {
            var response = await editArticleUseCase.Execute(request);
            if (response.Success)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", response.Response);
            return View(request);
        }

        [Authorize(Roles = "magasinier")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var response = await readArticleUseCase.Execute(new ReadArticleRequest(id.Value));
            if (!response.Success) return NotFound();
            var deleteRequest = new DeleteArticleRequest(response.Article.Id);
            return View(response.Article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteArticleRequest request)
        {
            var response = await deleteArticleUseCase.Execute(request);
            if (response.Success)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", response.Response);
            return View(request);
        }
    }
}