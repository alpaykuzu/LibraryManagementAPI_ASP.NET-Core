using BookManagementAPI.Data;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories.Implementations
{
    /// <summary>
    /// Ödünç alma kaydı (Enrollment) verilerine erişim sağlayan repository implementasyonu.
    /// </summary>
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        /// <summary>
        /// EnrollmentRepository constructor'ı. Temel repository işlemleri için context geçişi yapılır.
        /// </summary>
        /// <param name="context">Veritabanı context'i (LibraryDbContext)</param>
        public EnrollmentRepository(LibraryDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Belirli bir ödünç alma kaydını, öğrenci ve kitap detaylarıyla birlikte getirir.
        /// </summary>
        /// <param name="id">Ödünç alma kaydı ID'si</param>
        /// <returns>Student ve Book bilgileri dahil edilmiş Enrollment nesnesi. Bulunamazsa null döner.</returns>
        public async Task<Enrollment?> GetEnrollmentWithDetailsAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Student)    // İlgili öğrenci bilgisi dahil edilir
                .Include(e => e.Book)       // İlgili kitap bilgisi dahil edilir
                .FirstOrDefaultAsync(e => e.Id == id); // ID'ye göre ilk eşleşen ödünç kaydı bulunur
        }

        /// <summary>
        /// Tüm ödünç alma kayıtlarını öğrenci ve kitap detaylarıyla birlikte getirir.
        /// </summary>
        /// <returns>Öğrenci ve kitap bilgileri dahil edilmiş Enrollment listesi</returns>
        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsWithDetailsAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Student)    // Öğrenci bilgisi dahil edilir
                .Include(e => e.Book)       // Kitap bilgisi dahil edilir
                .ToListAsync();             // Tüm ödünç kayıtları liste halinde döner
        }

        /// <summary>
        /// Belirli bir kitabın hâlâ ödünçte olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="bookId">Kitap ID'si</param>
        /// <returns>Kitap hâlâ ödünçteyse true, iade edilmişse false döner</returns>
        public async Task<bool> IsBookBorrowedAsync(int bookId)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.BookId == bookId && !e.IsReturned); // İlgili kitap iade edilmemiş mi kontrolü
        }
    }
}
