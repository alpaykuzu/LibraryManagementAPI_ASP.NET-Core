using BookManagementAPI.DTOs;

namespace BookManagementAPI.Services.Interfaces
{
    /// <summary>
    /// Öğrenci işlemleri için servis arayüzü
    /// </summary>
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(StudentCreateDto studentDto);
        Task UpdateStudentAsync(int id, StudentUpdateDto studentDto);
        Task DeleteStudentAsync(int id);
    }
}