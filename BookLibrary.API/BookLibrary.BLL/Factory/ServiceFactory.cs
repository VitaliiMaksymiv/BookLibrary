using System;
using BookLibrary.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibrary.BLL.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private IAuthorService _authorService;

        private IBookService _bookService;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAuthorService AuthorService => _authorService ?? (_authorService = _serviceProvider.GetService<IAuthorService>());
        public IBookService BookService => _bookService ?? (_bookService = _serviceProvider.GetService<IBookService>());
    }
}
