using BookManagementAPI.Data;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories.Implementations
{
    /// <summary>
    /// Kitap (Book) verilerine erişim sağlayan repository implementasyonu.
    /// </summary>
    public class BookRepository : Repository<Book>, IBookRepository
    {
        /// <summary>
        /// BookRepository constructor'ı. Temel repository işlemleri için context geçişi yapılır.
        /// </summary>
        /// <param name="context">Veritabanı context'i (LibraryDbContext)</param>
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Belirli bir kitabın yazar ve kategori detaylarıyla birlikte getirir.
        /// </summary>
        /// <param name="id">Kitabın ID'si</param>
        /// <returns>Author ve Category bilgileriyle birlikte Book nesnesi. Bulunamazsa null döner.</returns>
        public async Task<Book?> GetBookWithDetailsAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)     // İlgili yazar bilgisi dahil edilir
                .Include(b => b.Category)   // İlgili kategori bilgisi dahil edilir
                .FirstOrDefaultAsync(b => b.Id == id); // ID'ye göre ilk eşleşen kitap alınır
        }

        /// <summary>
        /// Tüm kitapları yazar ve kategori detaylarıyla birlikte getirir.
        /// </summary>
        /// <returns>Author ve Category bilgileri dahil edilmiş kitap listesi</returns>
        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync()
        {
            return await _context.Books
                .Include(b => b.Author)     // Yazar bilgisi dahil edilir
                .Include(b => b.Category)   // Kategori bilgisi dahil edilir
                .ToListAsync();             // Tüm kitaplar liste halinde döner
        }

        /// <summary>
        /// Belirli bir kitabın herhangi bir kaydı (enrollment) olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="id">Kitabın ID'si</param>
        /// <returns>Kayıt varsa true, yoksa false döner</returns>
        public async Task<bool> HasEnrollmentsAsync(int id)
        {
            return await _context.Enrollments.AnyAsync(e => e.BookId == id); // Kitaba ait herhangi bir kayıt var mı kontrolü
        }
    }
}
