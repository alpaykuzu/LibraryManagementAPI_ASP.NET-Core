using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services.Implementations
{
    /// <summary>
    /// Ödünç alma kaydı işlemleri için servis implementasyonu
    /// </summary>
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _enrollmentRepository.GetAllEnrollmentsWithDetailsAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<EnrollmentDto> GetEnrollmentByIdAsync(int id)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentWithDetailsAsync(id);
            if (enrollment == null)
            {
                throw new KeyNotFoundException($"Ödünç alma kaydı bulunamadı ID: {id}");
            }
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<EnrollmentDto> CreateEnrollmentAsync(EnrollmentCreateDto enrollmentDto)
        {
            // Öğrenci ve kitap kontrolü
            var student = await _studentRepository.GetByIdAsync(enrollmentDto.StudentId);
            if (student == null)
            {
                throw new KeyNotFoundException($"Öğrenci bulunamadı ID: {enrollmentDto.StudentId}");
            }

            var book = await _bookRepository.GetByIdAsync(enrollmentDto.BookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Kitap bulunamadı ID: {enrollmentDto.BookId}");
            }

            // Kitabın ödünç alınma durumunu kontrol et
            if (await _enrollmentRepository.IsBookBorrowedAsync(enrollmentDto.BookId))
            {
                throw new InvalidOperationException("Bu kitap zaten başka bir öğrenciye ödünç verilmiş ve henüz iade edilmemiş.");
            }

            var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
            var createdEnrollment = await _enrollmentRepository.AddAsync(enrollment);

            // Eklenen kaydı detaylarıyla birlikte almak için
            var enrollmentWithDetails = await _enrollmentRepository.GetEnrollmentWithDetailsAsync(createdEnrollment.Id);
            return _mapper.Map<EnrollmentDto>(enrollmentWithDetails);
        }

        public async Task UpdateEnrollmentAsync(int id, EnrollmentUpdateDto enrollmentDto)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
            {
                throw new KeyNotFoundException($"Ödünç alma kaydı bulunamadı ID: {id}");
            }

            _mapper.Map(enrollmentDto, enrollment);
            await _enrollmentRepository.UpdateAsync(enrollment);
        }

        public async Task DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
            {
                throw new KeyNotFoundException($"Ödünç alma kaydı bulunamadı ID: {id}");
            }

            await _enrollmentRepository.DeleteAsync(enrollment);
        }
    }
}