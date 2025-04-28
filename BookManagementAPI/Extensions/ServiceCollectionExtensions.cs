using BookManagementAPI.Repositories.Implementations;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Implementations;
using BookManagementAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagementAPI.Extensions
{
    /// <summary>
    /// Dependency Injection için servis koleksiyonu genişletme sınıfı
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Repository bağımlılıklarını ekler
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            return services;
        }

        /// <summary>
        /// Service bağımlılıklarını ekler
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();

            return services;
        }
    }
}