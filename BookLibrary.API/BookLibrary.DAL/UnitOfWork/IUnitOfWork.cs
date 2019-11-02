using System.Threading.Tasks;
using BookLibrary.DAL.Repositories.InterfacesRepositories;

namespace BookLibrary.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthorRepository AuthorRepository { get; }

        IBookRepository BookRepository { get; }

        Task<int> SaveAsync();
    }
}