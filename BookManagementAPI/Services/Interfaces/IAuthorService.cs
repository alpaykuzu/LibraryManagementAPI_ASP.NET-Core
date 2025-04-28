using BookManagementAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagementAPI.Services.Interfaces
{
    /// <summary>
    /// Yazar işlemleri için servis arayüzü
    /// </summary>
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(int id);
        Task<AuthorDto> CreateAuthorAsync(AuthorCreateDto authorDto);
        Task UpdateAuthorAsync(int id, AuthorUpdateDto authorDto);
        Task DeleteAuthorAsync(int id);
    }
}