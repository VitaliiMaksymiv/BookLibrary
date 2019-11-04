using System.Linq;
using BookLibrary.DAL.Models;
using BookLibrary.DAL.Models.Entities;
using BookLibrary.DAL.Repositories.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.ImplementedRepositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookLibraryDbContext context) : base(context)
        {
        }

        protected override IQueryable<Book> ComplexEntities => Entities.AsNoTracking().
            Include(b => b.AuthorBooks).
            ThenInclude(b => b.Author).
            OrderByDescending(b => b.UpdatedDate).ThenByDescending(b => b.CreatedDate);
    }
}