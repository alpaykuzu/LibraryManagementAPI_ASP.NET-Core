using BookManagementAPI.Data;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories.Implementations
{
    /// <summary>
    /// Öğrenci (Student) repository implementasyonu.
    /// Öğrenciler ile ilgili veritabanı işlemleri burada yönetilir.
    /// </summary>
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        /// <summary>
        /// Öğrenci repository constructor'ı.
        /// </summary>
        /// <param name="context">Veritabanı context'i</param>
        public StudentRepository(LibraryDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Belirli bir öğrenciyi, kayıtlı olduğu kitaplarla (enrollments) birlikte getirir.
        /// </summary>
        /// <param name="id">Öğrenci ID</param>
        /// <returns>Öğrenci nesnesi veya null</returns>
        public async Task<Student?> GetStudentWithEnrollmentsAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Book)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Tüm öğrencileri, kayıtlı oldukları kitaplarla birlikte getirir.
        /// </summary>
        /// <returns>Öğrenci listesi</returns>
        public async Task<IEnumerable<Student>> GetAllStudentsWithEnrollmentsAsync()
        {
            return await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Book)
                .ToListAsync();
        }

        /// <summary>
        /// Belirli bir öğrencinin herhangi bir kaydı (enrollment) olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="id">Öğrenci ID</param>
        /// <returns>Eğer kaydı varsa true, yoksa false</returns>
        public async Task<bool> HasEnrollmentsAsync(int id)
        {
            return await _context.Enrollments.AnyAsync(e => e.StudentId == id);
        }
    }
}
