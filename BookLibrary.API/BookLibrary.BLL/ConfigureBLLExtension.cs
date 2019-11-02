using BookLibrary.BLL.Factory;
using BookLibrary.BLL.Services.Implemented;
using BookLibrary.BLL.Services.Interfaces;
using BookLibrary.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibrary.BLL
{
    public static class ConfigureBLLExtension
    {
        public static void ConfigureBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDAL(configuration);
            services.ConfigureServices();

            services.AddScoped<IServiceFactory, ServiceFactory>();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}