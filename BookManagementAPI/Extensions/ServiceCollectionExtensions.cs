using BookManagementAPI.Repositories.Implementations;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Implementations;
using BookManagementAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagementAPI.Extensions
{
    /// <summary>
    /// Bağımlılık enjeksiyonu (Dependency Injection - DI) için servis koleksiyonunu genişleten sınıf.
    /// Bu sınıf, uygulamanın ihtiyaç duyduğu tüm repository ve service bağımlılıklarını ekler.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Repository bağımlılıklarını servislere ekler.
        /// Repository, veri erişim katmanını temsil eder.
        /// </summary>
        /// <param name="services">Bağımlılıkların ekleneceği servis koleksiyonu.</param>
        /// <returns>Servis koleksiyonunu geri döner.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Repository'leri ekliyoruz.
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            return services;
        }

        /// <summary>
        /// Service bağımlılıklarını servislere ekler.
        /// Service, iş mantığı katmanını temsil eder ve repository'ler ile iletişim kurar.
        /// </summary>
        /// <param name="services">Bağımlılıkların ekleneceği servis koleksiyonu.</param>
        /// <returns>Servis koleksiyonunu geri döner.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Servisleri ekliyoruz.
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();

            return services;
        }
    }
}
