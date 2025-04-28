using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services.Implementations
{
    /// <summary>
    /// Kategori işlemleri için servis implementasyonu
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesWithBooksAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryWithBooksAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori bulunamadı ID: {id}");
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var createdCategory = await _categoryRepository.AddAsync(category);

            // Eklenen kategoriyi detaylarıyla birlikte almak için
            var categoryWithDetails = await _categoryRepository.GetCategoryWithBooksAsync(createdCategory.Id);
            return _mapper.Map<CategoryDto>(categoryWithDetails);
        }

        public async Task UpdateCategoryAsync(int id, CategoryUpdateDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori bulunamadı ID: {id}");
            }

            _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori bulunamadı ID: {id}");
            }

            // İlişkili kitapları kontrol et
            if (await _categoryRepository.HasBooksAsync(id))
            {
                throw new InvalidOperationException("Bu kategoriye ait kitaplar bulunmaktadır. Önce ilişkili kitapları silmelisiniz.");
            }

            await _categoryRepository.DeleteAsync(category);
        }
    }
}