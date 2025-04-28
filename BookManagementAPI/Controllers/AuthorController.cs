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
    /// Yazarlarla ilgili CRUD işlemlerini gerçekleştiren controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Tüm yazarları getir.
        /// </summary>
        /// <returns>Tüm yazarların DTO listesi</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        /// <summary>
        /// ID ile yazar getir.
        /// </summary>
        /// <param name="id">Yazarın ID'si</param>
        /// <returns>Yazarın DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Yazar bulunamadığında fırlatılır</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                return Ok(author);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Yazar güncelle.
        /// </summary>
        /// <param name="id">Güncellenecek yazarın ID'si</param>
        /// <param name="authorDto">Yazar güncelleme DTO'su</param>
        /// <returns>Güncelleme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Yazar bulunamadığında fırlatılır</exception>
        /// <exception cref="Exception">Diğer genel hatalar için fırlatılır</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            try
            {
                await _authorService.UpdateAuthorAsync(id, authorDto);
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
        /// Yeni yazar ekle.
        /// </summary>
        /// <param name="authorDto">Yazar oluşturma DTO'su</param>
        /// <returns>Oluşturulan yazarın DTO'su</returns>
        /// <exception cref="Exception">Genel hata durumları için fırlatılır</exception>
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            try
            {
                var author = await _authorService.CreateAuthorAsync(authorDto);
                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Yazar sil.
        /// </summary>
        /// <param name="id">Silinecek yazarın ID'si</param>
        /// <returns>Silme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Yazar bulunamadığında fırlatılır</exception>
        /// <exception cref="InvalidOperationException">Geçersiz işlem durumları için fırlatılır</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
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
