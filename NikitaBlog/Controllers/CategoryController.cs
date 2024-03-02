using Microsoft.AspNetCore.Mvc;
using NikitaBlog.Models;
using NikitaBlog.Repository.IRepository;

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

        
    }
}
