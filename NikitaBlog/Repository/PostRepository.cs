using NikitaBlog.Data;
using NikitaBlog.Models;
using NikitaBlog.Repository.IRepository;

namespace NikitaBlog.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
        }
    }
}
