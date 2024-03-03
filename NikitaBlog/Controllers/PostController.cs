using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Post> posts = _unitOfWork.Post.GetAll().ToList();
            
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            Post? post = _unitOfWork.Post.Get(p => p.Id == id);
            return View(post);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            });

            PostVM postVM = new()
            {
                Post = new Post(),
                CategoryList = CategoryList
            };

            if (id is null || id == 0)
            {
                return View(postVM);
            }
            else
            {
                postVM.Post = _unitOfWork.Post.Get(post => post.Id == id);
                return View(postVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(PostVM postVM, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                postVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                });
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if(file is not null)
            {
                string postPath = Path.Combine(wwwRootPath, $@"images\posts");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                using (FileStream fileStream = new FileStream(Path.Combine(postPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                postVM.Post.ImageUrl = @"\images\posts\" + fileName;
            }

            if(postVM.Post.Id == 0)
            {
                //create
                _unitOfWork.Post.Add(postVM.Post);
                _unitOfWork.Save();
                TempData["success"] = $"Post {postVM.Post.Title} created";
            }
            else
            {
                //update
                _unitOfWork.Post.Update(postVM.Post);
                _unitOfWork.Save();
                TempData["success"] = $"Post {postVM.Post.Title} updated";

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Post? post = _unitOfWork.Post.Get(p => p.Id == id);
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteAction(int id)
        {
            Post? post = _unitOfWork.Post.Get(p => p.Id == id);

            _unitOfWork.Post.Remove(post);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

    
    }
}
