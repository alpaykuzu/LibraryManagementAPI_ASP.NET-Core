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
    /// Kategorilerle ilgili CRUD işlemlerini gerçekleştiren controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Tüm kategorileri getir.
        /// </summary>
        /// <returns>Tüm kategorilerin DTO listesi</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// ID ile kategori getir.
        /// </summary>
        /// <param name="id">Kategorinin ID'si</param>
        /// <returns>Kategori DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Kategori bulunamadığında fırlatılır</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Kategori güncelle.
        /// </summary>
        /// <param name="id">Güncellenecek kategorinin ID'si</param>
        /// <param name="categoryDto">Kategori güncelleme DTO'su</param>
        /// <returns>Güncelleme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Kategori bulunamadığında fırlatılır</exception>
        /// <exception cref="Exception">Diğer genel hatalar için fırlatılır</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryUpdateDto categoryDto)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(id, categoryDto);
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
        /// Yeni kategori ekle.
        /// </summary>
        /// <param name="categoryDto">Kategori oluşturma DTO'su</param>
        /// <returns>Oluşturulan kategorinin DTO'su</returns>
        /// <exception cref="Exception">Genel hata durumları için fırlatılır</exception>
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> PostCategory(CategoryCreateDto categoryDto)
        {
            try
            {
                var category = await _categoryService.CreateCategoryAsync(categoryDto);
                return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Kategori sil.
        /// </summary>
        /// <param name="id">Silinecek kategorinin ID'si</param>
        /// <returns>Silme işlemine ait HTTP yanıtı</returns>
        /// <exception cref="KeyNotFoundException">Kategori bulunamadığında fırlatılır</exception>
        /// <exception cref="InvalidOperationException">Geçersiz işlem durumu (örneğin, ilişkili veri bulunması gibi) için fırlatılır</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
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
