using BookLibrary.DAL.Models;
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
        }

        private static void ConfigureDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            void ConfigureConnection(DbContextOptionsBuilder options)
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            }

            services.AddDbContext<BookLibraryDbContext>(ConfigureConnection);
        }
    }
}
