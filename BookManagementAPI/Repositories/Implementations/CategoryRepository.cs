using BookManagementAPI.Data;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories.Implementations
{
    /// <summary>
    /// Kategori (Category) verilerine erişim sağlayan repository implementasyonu.
    /// </summary>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        /// <summary>
        /// CategoryRepository constructor'ı. Temel repository işlemleri için context geçişi yapılır.
        /// </summary>
        /// <param name="context">Veritabanı context'i (LibraryDbContext)</param>
        public CategoryRepository(LibraryDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Belirli bir kategoriyi, ilişkili kitaplarıyla birlikte getirir.
        /// </summary>
        /// <param name="id">Kategori ID'si</param>
        /// <returns>Kitapları dahil edilmiş Category nesnesi. Bulunamazsa null döner.</returns>
        public async Task<Category?> GetCategoryWithBooksAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Books)    // İlgili kitapları dahil eder
                .FirstOrDefaultAsync(c => c.Id == id); // ID'ye göre ilk eşleşen kategori bulunur
        }

        /// <summary>
        /// Tüm kategorileri, ilişkili kitaplarıyla birlikte getirir.
        /// </summary>
        /// <returns>Kitapları dahil edilmiş Category listesi</returns>
        public async Task<IEnumerable<Category>> GetAllCategoriesWithBooksAsync()
        {
            return await _context.Categories
                .Include(c => c.Books)    // İlgili kitapları dahil eder
                .ToListAsync();           // Tüm kategorileri getirir
        }

        /// <summary>
        /// Belirli bir kategoriye ait herhangi bir kitap olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="id">Kategori ID'si</param>
        /// <returns>Kitap varsa true, yoksa false döner</returns>
        public async Task<bool> HasBooksAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.CategoryId == id); // İlgili kategoriye ait kitap var mı kontrolü
        }
    }
}
