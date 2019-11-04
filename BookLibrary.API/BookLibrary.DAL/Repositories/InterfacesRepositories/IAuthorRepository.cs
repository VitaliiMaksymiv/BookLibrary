using System.Threading.Tasks;
using BookLibrary.DAL.Models.Entities;

namespace BookLibrary.DAL.Repositories.InterfacesRepositories
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<Author> AttachBook(int authorId, int bookId);

        Task<Author> TrackingGetByIdAsync(int id);
    }
}