using BookManagementAPI.Data;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories.Implementations
{
    /// <summary>
    /// Yazar (Author) verilerine erişim sağlayan repository implementasyonu.
    /// </summary>
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        /// <summary>
        /// AuthorRepository constructor'ı. Temel repository işlemleri için context geçişi yapılır.
        /// </summary>
        /// <param name="context">Veritabanı context'i (LibraryDbContext)</param>
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Belirli bir yazarın kitaplarıyla birlikte detaylarını getirir.
        /// </summary>
        /// <param name="id">Yazarın ID'si</param>
        /// <returns>Kitapları dahil edilmiş Author nesnesi</returns>
        public async Task<Author?> GetAuthorWithBooksAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books) // İlişkili kitapları da dahil eder
                .FirstOrDefaultAsync(a => a.Id == id); // İlgili ID'ye sahip yazarı getirir
        }

        /// <summary>
        /// Tüm yazarları kitaplarıyla birlikte getirir.
        /// </summary>
        /// <returns>Kitapları dahil edilmiş yazar listesi</returns>
        public async Task<IEnumerable<Author>> GetAllAuthorsWithBooksAsync()
        {
            return await _context.Authors
                .Include(a => a.Books) // İlişkili kitapları dahil eder
                .ToListAsync(); // Tüm yazarları getirir
        }

        /// <summary>
        /// Belirli bir yazarın herhangi bir kitabı olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="id">Yazarın ID'si</param>
        /// <returns>Kitap varsa true, yoksa false döner</returns>
        public async Task<bool> HasBooksAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.AuthorId == id); // İlgili yazara ait kitap var mı kontrolü
        }
    }
}
