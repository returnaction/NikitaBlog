using Microsoft.AspNetCore.Mvc;
using NikitaBlog.Models;
using NikitaBlog.Repository.IRepository;
using System.Security.Permissions;

namespace NikitaBlog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Upsert(int? id)
        {
            if(id is null || id == 0)
            {
                Category category = new();
                return View(category);
            }
            else
            {
                Category? category = _unitOfWork.Category.Get(c => c.Id == id);
                return View(category);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if(category.Id == 0)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteAction(int id)
        {
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);
            if(category is null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        
    }
}
