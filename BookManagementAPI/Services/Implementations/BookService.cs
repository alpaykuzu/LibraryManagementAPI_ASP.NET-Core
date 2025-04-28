using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services.Implementations
{
    /// <summary>
    /// Kitap işlemleri için servis implementasyonu
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

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksWithDetailsAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookWithDetailsAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Kitap bulunamadı ID: {id}");
            }
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateBookAsync(BookCreateDto bookDto)
        {
            // Yazar ve kategori kontrolü
            var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
            if (author == null)
            {
                throw new KeyNotFoundException($"Yazar bulunamadı ID: {bookDto.AuthorId}");
            }

            var category = await _categoryRepository.GetByIdAsync(bookDto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori bulunamadı ID: {bookDto.CategoryId}");
            }

            var book = _mapper.Map<Book>(bookDto);
            var createdBook = await _bookRepository.AddAsync(book);

            // Eklenen kitabı detaylarıyla birlikte almak için
            var bookWithDetails = await _bookRepository.GetBookWithDetailsAsync(createdBook.Id);
            return _mapper.Map<BookDto>(bookWithDetails);
        }

        public async Task UpdateBookAsync(int id, BookUpdateDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Kitap bulunamadı ID: {id}");
            }

            // Yazar ve kategori kontrolü
            var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
            if (author == null)
            {
                throw new KeyNotFoundException($"Yazar bulunamadı ID: {bookDto.AuthorId}");
            }

            var category = await _categoryRepository.GetByIdAsync(bookDto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori bulunamadı ID: {bookDto.CategoryId}");
            }

            _mapper.Map(bookDto, book);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Kitap bulunamadı ID: {id}");
            }

            // İlişkili ödünç alma kayıtlarını kontrol et
            if (await _bookRepository.HasEnrollmentsAsync(id))
            {
                throw new InvalidOperationException("Bu kitaba ait ödünç alma kayıtları bulunmaktadır. Önce ilişkili kayıtları silmelisiniz.");
            }

            await _bookRepository.DeleteAsync(book);
        }
    }
}