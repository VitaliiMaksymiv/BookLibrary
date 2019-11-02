using BookLibrary.BLL.Services.Interfaces;

namespace BookLibrary.BLL.Factory
{
    public interface IServiceFactory
    {
        IAuthorService AuthorService { get; }

        IBookService BookService { get; }
    }
}