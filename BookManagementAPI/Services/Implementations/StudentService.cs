using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services.Implementations
{
    /// <summary>
    /// Öğrenci işlemleri için servis implementasyonu
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

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsWithEnrollmentsAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentWithEnrollmentsAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Öğrenci bulunamadı ID: {id}");
            }
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> CreateStudentAsync(StudentCreateDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            var createdStudent = await _studentRepository.AddAsync(student);

            // Eklenen öğrenciyi detaylarıyla birlikte almak için
            var studentWithDetails = await _studentRepository.GetStudentWithEnrollmentsAsync(createdStudent.Id);
            return _mapper.Map<StudentDto>(studentWithDetails);
        }

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