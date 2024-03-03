using NikitaBlog.Models;

namespace NikitaBlog.Repository.IRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        void Update(Post post);
    }
}
