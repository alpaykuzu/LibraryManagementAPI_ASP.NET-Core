using BookManagementAPI.Entities;

namespace BookManagementAPI.Repositories.Interfaces
{
    /// <summary>
    /// Ödünç alma kaydı veri tabanı işlemleri için özel repository arayüzü
    /// </summary>
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<Enrollment> GetEnrollmentWithDetailsAsync(int id);
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsWithDetailsAsync();
        Task<bool> IsBookBorrowedAsync(int bookId);
    }
}