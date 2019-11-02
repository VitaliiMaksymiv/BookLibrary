using AutoMapper;
using BookLibrary.BLL.Factory;
using BookLibrary.BLL.Mappings;
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
            services.ConfigureAutoMapper();
            services.AddScoped<IServiceFactory, ServiceFactory>();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
        }

        private static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
            {
                c.AddProfile(new AuthorProfile());
                c.AddProfile(new BookProfile());
            }).CreateMapper());
        }
    }
}