using BookManagementAPI.DTOs;

namespace BookManagementAPI.Services.Interfaces
{
    /// <summary>
    /// Kategori işlemleri için servis arayüzü
    /// </summary>
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryDto);
        Task UpdateCategoryAsync(int id, CategoryUpdateDto categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}