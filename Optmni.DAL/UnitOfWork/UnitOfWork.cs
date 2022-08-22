using System.Threading.Tasks;

namespace Optmni.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private OptmniDbContext _context;

        public UnitOfWork(OptmniDbContext contex)
        {
            _context = contex;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
