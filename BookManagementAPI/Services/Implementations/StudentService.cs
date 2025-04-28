using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services.Implementations
{
    /// <summary>
    /// Öğrenci işlemleri için servis implementasyonu.
    /// Öğrenci CRUD operasyonlarını yönetir.
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm öğrencileri ve ilişkili ödünç alma bilgilerini alır.
        /// </summary>
        /// <returns>Öğrencilerin DTO listesi</returns>
        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsWithEnrollmentsAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        /// <summary>
        /// Öğrenciyi ID'ye göre getirir.
        /// </summary>
        /// <param name="id">Öğrencinin ID'si</param>
        /// <returns>Öğrencinin DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Öğrenci bulunamadığında fırlatılır</exception>
        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentWithEnrollmentsAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Öğrenci bulunamadı ID: {id}");
            }
            return _mapper.Map<StudentDto>(student);
        }

        /// <summary>
        /// Yeni bir öğrenci oluşturur.
        /// </summary>
        /// <param name="studentDto">Öğrenci oluşturma DTO'su</param>
        /// <returns>Oluşturulan öğrencinin DTO'su</returns>
        public async Task<StudentDto> CreateStudentAsync(StudentCreateDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            var createdStudent = await _studentRepository.AddAsync(student);

            // Eklenen öğrenciyi detaylarıyla birlikte almak için
            var studentWithDetails = await _studentRepository.GetStudentWithEnrollmentsAsync(createdStudent.Id);
            return _mapper.Map<StudentDto>(studentWithDetails);
        }

        /// <summary>
        /// Var olan öğrenciyi günceller.
        /// </summary>
        /// <param name="id">Güncellenecek öğrencinin ID'si</param>
        /// <param name="studentDto">Öğrenci güncelleme DTO'su</param>
        /// <exception cref="KeyNotFoundException">Öğrenci bulunamadığında fırlatılır</exception>
        public async Task UpdateStudentAsync(int id, StudentUpdateDto studentDto)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Öğrenci bulunamadı ID: {id}");
            }

            _mapper.Map(studentDto, student);
            await _studentRepository.UpdateAsync(student);
        }

        /// <summary>
        /// Öğrenciyi siler.
        /// </summary>
        /// <param name="id">Silinecek öğrencinin ID'si</param>
        /// <exception cref="KeyNotFoundException">Öğrenci bulunamadığında fırlatılır</exception>
        /// <exception cref="InvalidOperationException">Öğrencinin ilişkili ödünç alma kayıtları varsa fırlatılır</exception>
        public async Task DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Öğrenci bulunamadı ID: {id}");
            }

            // İlişkili ödünç alma kayıtlarını kontrol et
            if (await _studentRepository.HasEnrollmentsAsync(id))
            {
                throw new InvalidOperationException("Bu öğrenciye ait ödünç alma kayıtları bulunmaktadır. Önce ilişkili kayıtları silmelisiniz.");
            }

            await _studentRepository.DeleteAsync(student);
        }
    }
}
