using BookManagementAPI.Entities;

namespace BookManagementAPI.Repositories.Interfaces
{
    /// <summary>
    /// Öğrenci veri tabanı işlemleri için özel repository arayüzü
    /// </summary>
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetStudentWithEnrollmentsAsync(int id);
        Task<IEnumerable<Student>> GetAllStudentsWithEnrollmentsAsync();
        Task<bool> HasEnrollmentsAsync(int id);
    }
}