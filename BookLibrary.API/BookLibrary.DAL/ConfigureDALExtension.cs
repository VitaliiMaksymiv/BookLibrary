using BookLibrary.DAL.Models;
using BookLibrary.DAL.Models.Entities;
using BookLibrary.DAL.Repositories;
using BookLibrary.DAL.Repositories.ImplementedRepositories;
using BookLibrary.DAL.Repositories.InterfacesRepositories;
using BookLibrary.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibrary.DAL
{
    public static class ConfigureDALExtension
    {
        public static void ConfigureDAL(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.ConfigureDbContext(configuration);

            services.ConfigureRepositories();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }

        private static void ConfigureDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            void ConfigureConnection(DbContextOptionsBuilder options)
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            }

            services.AddDbContext<BookLibraryDbContext>(ConfigureConnection);
        }

        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
