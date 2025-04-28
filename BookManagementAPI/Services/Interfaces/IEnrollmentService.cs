using BookManagementAPI.DTOs;

namespace BookManagementAPI.Services.Interfaces
{
    /// <summary>
    /// Ödünç alma kaydı işlemleri için servis arayüzü
    /// </summary>
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task<EnrollmentDto> GetEnrollmentByIdAsync(int id);
        Task<EnrollmentDto> CreateEnrollmentAsync(EnrollmentCreateDto enrollmentDto);
        Task UpdateEnrollmentAsync(int id, EnrollmentUpdateDto enrollmentDto);
        Task DeleteEnrollmentAsync(int id);
    }
}