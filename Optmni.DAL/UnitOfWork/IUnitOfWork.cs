using System;
using System.Threading.Tasks;

namespace Optmni.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
