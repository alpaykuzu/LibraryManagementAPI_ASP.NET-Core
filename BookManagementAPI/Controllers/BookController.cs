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
    /// Kitaplarla ilgili CRUD işlemlerini gerçekleştiren controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Tüm kitapları getir.
        /// </summary>
        /// <returns>Tüm kitapların DTO listesi</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// ID ile kitap getir.
        /// </summary>
        /// <param name="id">Kitabın ID'si</param>
        /// <returns>Kitabın DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Kitap bulunamadığında fırlatılır</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Kitap güncelle.
        /// </summary>
        /// <param name="id">Güncellenecek kitabın ID'si</param>
        /// <param name="bookDto">Kitap güncelleme DTO'su</param>
        /// <returns>Güncelleme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Kitap bulunamadığında fırlatılır</exception>
        /// <exception cref="Exception">Diğer genel hatalar için fırlatılır</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, bookDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                if (ex.Message.Contains("Kitap"))
                {
                    return NotFound();
                }
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Yeni kitap ekle.
        /// </summary>
        /// <param name="bookDto">Kitap oluşturma DTO'su</param>
        /// <returns>Oluşturulan kitabın DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Kitapla ilgili hata durumlarında fırlatılır</exception>
        /// <exception cref="Exception">Genel hata durumları için fırlatılır</exception>
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(BookCreateDto bookDto)
        {
            try
            {
                var book = await _bookService.CreateBookAsync(bookDto);
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Kitap sil.
        /// </summary>
        /// <param name="id">Silinecek kitabın ID'si</param>
        /// <returns>Silme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Kitap bulunamadığında fırlatılır</exception>
        /// <exception cref="InvalidOperationException">Geçersiz işlem durumu (örneğin, ilişkili veri bulunması gibi) için fırlatılır</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
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
