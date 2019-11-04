using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookLibrary.DAL.Models;
using BookLibrary.DAL.Models.Entities;
using BookLibrary.DAL.Repositories.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.ImplementedRepositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly BookLibraryDbContext _context;
        public AuthorRepository(BookLibraryDbContext context) : base(context)
        {
            _context = context;
        }

        protected override IQueryable<Author> ComplexEntities => Entities.AsNoTracking().
            Include(a => a.AuthorBooks).
            ThenInclude(a => a.Book).
            OrderByDescending(a => a.UpdatedDate).ThenByDescending(a => a.CreatedDate);

        protected IQueryable<Author> TrackingComplexEntities => Entities.
            Include(a => a.AuthorBooks).
            ThenInclude(a => a.Book).
            OrderByDescending(a => a.UpdatedDate).ThenByDescending(a => a.CreatedDate);

        public  Task<Author> TrackingGetByIdAsync(int id)
        {
            return TrackingComplexEntities.SingleOrDefaultAsync(entity => entity.Id == id);
        }
        public async Task<Author> AttachBook(int authorId, int bookId)
        {
            var author = await TrackingGetByIdAsync(authorId);
            author.AuthorBooks.Add(new AuthorBook { AuthorId = authorId, BookId = bookId});
            return author;
        }
    }
}