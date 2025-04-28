using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagementAPI.Services.Implementations
{
    /// <summary>
    /// Kitap işlemleri için servis implementasyonu.
    /// Kitap (Book) CRUD operasyonlarını yönetir.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm kitapları ve ilişkili detayları alır.
        /// </summary>
        /// <returns>Kitapların DTO listesi</returns>
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksWithDetailsAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        /// <summary>
        /// Kitap ID'sine göre kitap detaylarını getirir.
        /// </summary>
        /// <param name="id">Kitap ID'si</param>
        /// <returns>Kitap detaylarının DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Kitap bulunamadığında fırlatılır</exception>
        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookWithDetailsAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Kitap bulunamadı ID: {id}");
            }
            return _mapper.Map<BookDto>(book);
        }

        /// <summary>
        /// Yeni bir kitap oluşturur.
        /// Yazar ve kategori kontrolü yapılır.
        /// </summary>
        /// <param name="bookDto">Kitap oluşturma DTO'su</param>
        /// <returns>Oluşturulan kitabın DTO'su</returns>
        /// <exception cref="KeyNotFoundException">Yazar veya kategori bulunamazsa fırlatılır</exception>
        public async Task<BookDto> CreateBookAsync(BookCreateDto bookDto)
        {
            await ValidateAuthorAndCategoryAsync(bookDto.AuthorId, bookDto.CategoryId);

            var book = _mapper.Map<Book>(bookDto);
            var createdBook = await _bookRepository.AddAsync(book);

            // Eklenen kitabı detaylarıyla birlikte almak için
            var bookWithDetails = await _bookRepository.GetBookWithDetailsAsync(createdBook.Id);
            return _mapper.Map<BookDto>(bookWithDetails);
        }

        /// <summary>
        /// Kitap bilgilerini günceller.
        /// Yazar ve kategori kontrolü yapılır.
        /// </summary>
        /// <param name="id">Güncellenecek kitabın ID'si</param>
        /// <param name="bookDto">Kitap güncelleme DTO'su</param>
        /// <exception cref="KeyNotFoundException">Kitap, yazar veya kategori bulunamazsa fırlatılır</exception>
        public async Task UpdateBookAsync(int id, BookUpdateDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Kitap bulunamadı ID: {id}");
            }

            await ValidateAuthorAndCategoryAsync(bookDto.AuthorId, bookDto.CategoryId);

            _mapper.Map(bookDto, book);
            await _bookRepository.UpdateAsync(book);
        }

        /// <summary>
        /// Kitap silme işlemi.
        /// İlişkili ödünç alma kayıtları kontrol edilir.
        /// </summary>
        /// <param name="id">Silinecek kitabın ID'si</param>
        /// <exception cref="KeyNotFoundException">Kitap bulunamazsa fırlatılır</exception>
        /// <exception cref="InvalidOperationException">Kitap ödünç alındıysa fırlatılır</exception>
        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Kitap bulunamadı ID: {id}");
            }

            if (await _bookRepository.HasEnrollmentsAsync(id))
            {
                throw new InvalidOperationException("Bu kitaba ait ödünç alma kayıtları bulunmaktadır. Önce ilişkili kayıtları silmelisiniz.");
            }

            await _bookRepository.DeleteAsync(book);
        }

        /// <summary>
        /// Yazar ve kategori ID'lerini kontrol eden yardımcı metod.
        /// </summary>
        /// <param name="authorId">Yazar ID'si</param>
        /// <param name="categoryId">Kategori ID'si</param>
        /// <exception cref="KeyNotFoundException">Yazar veya kategori bulunamazsa fırlatılır</exception>
        private async Task ValidateAuthorAndCategoryAsync(int authorId, int categoryId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                throw new KeyNotFoundException($"Yazar bulunamadı ID: {authorId}");
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori bulunamadı ID: {categoryId}");
            }
        }
    }
}
