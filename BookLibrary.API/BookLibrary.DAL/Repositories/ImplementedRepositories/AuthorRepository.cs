using System;
using System.Linq;
using System.Linq.Expressions;
using BookLibrary.DAL.Models;
using BookLibrary.DAL.Models.Entities;
using BookLibrary.DAL.Repositories.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.ImplementedRepositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookLibraryDbContext context) : base(context)
        {
        }

        public override Expression<Func<Author, bool>> MakeFilteringExpression(string keyword)
        {
            return author => EF.Functions.Like(author.Name, '%' + keyword + '%');
        }

        protected override IQueryable<Author> ComplexEntities => Entities.
            Include(a => a.AuthorBooks).
            ThenInclude(a => a.Book).
            OrderByDescending(a => a.UpdatedDate).ThenByDescending(a => a.CreatedDate);
    }
}