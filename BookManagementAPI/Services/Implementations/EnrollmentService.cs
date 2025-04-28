using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services.Implementations
{
    /// <summary>
    /// Ödünç alma kaydı işlemleri için servis implementasyonu.
    /// Ödünç alma (enrollment) CRUD operasyonlarını yönetir.
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

        /// <summary>
        /// Tüm ödünç alma kayıtlarını ve ilişkili öğrenci ile kitap bilgilerini alır.
        /// </summary>
        /// <returns>Ödünç alma kayıtlarının DTO listesi</returns>
        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _enrollmentRepository.GetAllEnrollmentsWithDetailsAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        /// <summary>
        /// Ödünç alma kaydını ID'ye göre getirir.
        /// </summary>
        /// <param name="id">Ödünç alma kaydının ID'si</param>
        /// <returns>Ödünç alma kaydının DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Ödünç alma kaydı bulunamadığında fırlatılır</exception>
        public async Task<EnrollmentDto> GetEnrollmentByIdAsync(int id)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentWithDetailsAsync(id);
            if (enrollment == null)
            {
                throw new KeyNotFoundException($"Ödünç alma kaydı bulunamadı ID: {id}");
            }
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        /// <summary>
        /// Yeni bir ödünç alma kaydı oluşturur.
        /// </summary>
        /// <param name="enrollmentDto">Ödünç alma kaydı oluşturma DTO'su</param>
        /// <returns>Oluşturulan ödünç alma kaydının DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Öğrenci veya kitap bulunamadığında fırlatılır</exception>
        /// <exception cref="InvalidOperationException">Kitap zaten ödünç alınmışsa fırlatılır</exception>
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

        /// <summary>
        /// Var olan ödünç alma kaydını günceller.
        /// </summary>
        /// <param name="id">Güncellenecek ödünç alma kaydının ID'si</param>
        /// <param name="enrollmentDto">Ödünç alma kaydı güncelleme DTO'su</param>
        /// <exception cref="KeyNotFoundException">Ödünç alma kaydı bulunamadığında fırlatılır</exception>
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

        /// <summary>
        /// Ödünç alma kaydını siler.
        /// </summary>
        /// <param name="id">Silinecek ödünç alma kaydının ID'si</param>
        /// <exception cref="KeyNotFoundException">Ödünç alma kaydı bulunamadığında fırlatılır</exception>
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
