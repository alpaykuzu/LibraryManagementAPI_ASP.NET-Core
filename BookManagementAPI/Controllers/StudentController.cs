using AutoMapper;
using BookManagementAPI.Data;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagementAPI.Controllers
{
    /// <summary>
    /// Öğrencilerle ilgili CRUD işlemlerini gerçekleştiren controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Tüm öğrencileri getir.
        /// </summary>
        /// <returns>Tüm öğrencilerin DTO listesi</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        /// <summary>
        /// ID ile öğrenci getir.
        /// </summary>
        /// <param name="id">Öğrencinin ID'si</param>
        /// <returns>Öğrenci DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Öğrenci bulunamadığında fırlatılır</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                return Ok(student);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Öğrenci güncelle.
        /// </summary>
        /// <param name="id">Güncellenecek öğrencinin ID'si</param>
        /// <param name="studentDto">Öğrenci güncelleme DTO'su</param>
        /// <returns>Güncelleme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Öğrenci bulunamadığında fırlatılır</exception>
        /// <exception cref="Exception">Diğer hata durumları için fırlatılır</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentUpdateDto studentDto)
        {
            try
            {
                await _studentService.UpdateStudentAsync(id, studentDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Yeni öğrenci ekle.
        /// </summary>
        /// <param name="studentDto">Yeni öğrenci DTO'su</param>
        /// <returns>Yeni öğrencinin DTO'su</returns>
        /// <exception cref="Exception">Genel hata durumları için fırlatılır</exception>
        [HttpPost]
        public async Task<ActionResult<StudentDto>> PostStudent(StudentCreateDto studentDto)
        {
            try
            {
                var student = await _studentService.CreateStudentAsync(studentDto);
                return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Öğrenci sil.
        /// </summary>
        /// <param name="id">Silinecek öğrencinin ID'si</param>
        /// <returns>Silme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Öğrenci bulunamadığında fırlatılır</exception>
        /// <exception cref="InvalidOperationException">Geçersiz işlem durumu için fırlatılır</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
