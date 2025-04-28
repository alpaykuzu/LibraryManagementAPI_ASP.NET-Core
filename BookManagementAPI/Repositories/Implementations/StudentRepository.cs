using BookManagementAPI.Data;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories.Implementations
{
    /// <summary>
    /// Öğrenci repository implementasyonu
    /// </summary>
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<Student> GetStudentWithEnrollmentsAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Book)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsWithEnrollmentsAsync()
        {
            return await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Book)
                .ToListAsync();
        }

        public async Task<bool> HasEnrollmentsAsync(int id)
        {
            return await _context.Enrollments.AnyAsync(e => e.StudentId == id);
        }
    }
}