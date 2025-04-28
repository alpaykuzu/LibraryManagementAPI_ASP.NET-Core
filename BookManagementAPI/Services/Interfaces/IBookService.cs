using BookManagementAPI.DTOs;

namespace BookManagementAPI.Services.Interfaces
{
    /// <summary>
    /// Kitap işlemleri için servis arayüzü
    /// </summary>
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(BookCreateDto bookDto);
        Task UpdateBookAsync(int id, BookUpdateDto bookDto);
        Task DeleteBookAsync(int id);
    }
}