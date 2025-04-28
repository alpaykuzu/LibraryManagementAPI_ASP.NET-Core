using BookManagementAPI.Data;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories.Implementations
{
    /// <summary>
    /// Ödünç alma kaydı repository implementasyonu
    /// </summary>
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<Enrollment> GetEnrollmentWithDetailsAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Book)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsWithDetailsAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Book)
                .ToListAsync();
        }

        public async Task<bool> IsBookBorrowedAsync(int bookId)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.BookId == bookId && !e.IsReturned);
        }
    }
}