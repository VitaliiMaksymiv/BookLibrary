using System.Threading.Tasks;
using BookLibrary.BLL.DTOs;

namespace BookLibrary.BLL.Services.Interfaces
{
    public interface IAuthorService : ICrudService<AuthorDTO>
    {
        Task<AuthorDTO> AttachBook(int authorId, int bookId);
    }
}