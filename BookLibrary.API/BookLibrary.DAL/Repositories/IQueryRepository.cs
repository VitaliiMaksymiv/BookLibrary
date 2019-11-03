using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Repositories
{
    public interface IQueryRepository<TEntity>
    {
        IQueryable<TEntity> GetQueryable();
        Task<IQueryable<TEntity>> SearchAsync(string str);
    }
}