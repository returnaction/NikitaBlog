using NikitaBlog.Data;
using NikitaBlog.Repository.IRepository;

namespace NikitaBlog.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
