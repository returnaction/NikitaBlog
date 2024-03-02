using NikitaBlog.Models;

namespace NikitaBlog.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
