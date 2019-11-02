using System;
using System.Threading.Tasks;
using BookLibrary.DAL.Models;
using BookLibrary.DAL.Repositories.InterfacesRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibrary.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookLibraryDbContext _context;

        private readonly IServiceProvider _serviceProvider;

        private IAuthorRepository _authorRepository;

        private IBookRepository _bookRepository;

        public UnitOfWork(BookLibraryDbContext context,
            IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IAuthorRepository AuthorRepository =>
            _authorRepository ?? (_authorRepository = _serviceProvider.GetService<IAuthorRepository>());

        public IBookRepository BookRepository =>
            _bookRepository ?? (_bookRepository = _serviceProvider.GetService<IBookRepository>());

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}