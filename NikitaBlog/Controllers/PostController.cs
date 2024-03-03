using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NikitaBlog.Data;
using NikitaBlog.Models;
using NikitaBlog.Repository.IRepository;

namespace NikitaBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public IActionResult Index()
        {
            List<Post> posts = _unitOfWork.Post.GetAll().ToList();
            
            return View(posts);
        }
    }
}
