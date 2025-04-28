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
    /// Enrollment (Öğrenci-Kitap kaydı) işlemlerini yöneten controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        /// <summary>
        /// Tüm ödünç alma kayıtlarını getir.
        /// </summary>
        /// <returns>Ödünç alma kayıtlarının DTO listesi</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return Ok(enrollments);
        }

        /// <summary>
        /// ID ile ödünç alma kaydını getir.
        /// </summary>
        /// <param name="id">Ödünç alma kaydının ID'si</param>
        /// <returns>Ödünç alma kaydının DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Ödünç alma kaydı bulunamadığında fırlatılır</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDto>> GetEnrollment(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
                return Ok(enrollment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Ödünç alma kaydını güncelle (kitap iade durumu).
        /// </summary>
        /// <param name="id">Güncellenecek ödünç alma kaydının ID'si</param>
        /// <param name="enrollmentDto">Ödünç alma kaydı güncelleme DTO'su</param>
        /// <returns>Güncelleme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Ödünç alma kaydı bulunamadığında fırlatılır</exception>
        /// <exception cref="Exception">Diğer genel hatalar için fırlatılır</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, EnrollmentUpdateDto enrollmentDto)
        {
            try
            {
                await _enrollmentService.UpdateEnrollmentAsync(id, enrollmentDto);
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
        /// Yeni ödünç alma kaydı ekle.
        /// </summary>
        /// <param name="enrollmentDto">Yeni ödünç alma kaydı DTO'su</param>
        /// <returns>Yeni ödünç alma kaydının DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Bağlantılı öğeler bulunamadığında fırlatılır</exception>
        /// <exception cref="InvalidOperationException">Geçersiz işlem durumu (örneğin, zaten var olan bir kayıt ekleme) için fırlatılır</exception>
        /// <exception cref="Exception">Genel hata durumları için fırlatılır</exception>
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> PostEnrollment(EnrollmentCreateDto enrollmentDto)
        {
            try
            {
                var enrollment = await _enrollmentService.CreateEnrollmentAsync(enrollmentDto);
                return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.Id }, enrollment);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ödünç alma kaydını sil.
        /// </summary>
        /// <param name="id">Silinecek ödünç alma kaydının ID'si</param>
        /// <returns>Silme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Ödünç alma kaydı bulunamadığında fırlatılır</exception>
        /// <exception cref="Exception">Genel hata durumları için fırlatılır</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            try
            {
                await _enrollmentService.DeleteEnrollmentAsync(id);
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
    }
}
