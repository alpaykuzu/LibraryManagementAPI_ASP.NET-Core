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
    /// Yazar işlemleri için servis implementasyonu.
    /// Yazar (Author) CRUD operasyonlarını yönetir.
    /// </summary>
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// AuthorService constructor'ı.
        /// </summary>
        /// <param name="authorRepository">Yazar repository'si</param>
        /// <param name="mapper">AutoMapper nesnesi</param>
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm yazarları ve yazdıkları kitapları getirir.
        /// </summary>
        /// <returns>Yazar DTO listesi</returns>
        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsWithBooksAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        /// <summary>
        /// Belirli bir yazarı ID'sine göre getirir (kitap detaylarıyla birlikte).
        /// </summary>
        /// <param name="id">Yazar ID</param>
        /// <returns>Yazar DTO</returns>
        /// <exception cref="KeyNotFoundException">Eğer yazar bulunamazsa fırlatılır</exception>
        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAuthorWithBooksAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Yazar bulunamadı ID: {id}");
            }
            return _mapper.Map<AuthorDto>(author);
        }

        /// <summary>
        /// Yeni bir yazar oluşturur.
        /// </summary>
        /// <param name="authorDto">Yazar oluşturma DTO'su</param>
        /// <returns>Oluşturulan yazar DTO'su</returns>
        public async Task<AuthorDto> CreateAuthorAsync(AuthorCreateDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            var createdAuthor = await _authorRepository.AddAsync(author);

            // Eklenen yazarı, kitap detayları ile birlikte tekrar çekiyoruz
            var authorWithDetails = await _authorRepository.GetAuthorWithBooksAsync(createdAuthor.Id);
            return _mapper.Map<AuthorDto>(authorWithDetails);
        }

        /// <summary>
        /// Var olan bir yazarı günceller.
        /// </summary>
        /// <param name="id">Yazar ID</param>
        /// <param name="authorDto">Güncellenecek yazar DTO'su</param>
        /// <exception cref="KeyNotFoundException">Eğer yazar bulunamazsa fırlatılır</exception>
        public async Task UpdateAuthorAsync(int id, AuthorUpdateDto authorDto)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Yazar bulunamadı ID: {id}");
            }

            _mapper.Map(authorDto, author);
            await _authorRepository.UpdateAsync(author);
        }

        /// <summary>
        /// Belirli bir yazarı siler.
        /// Önce yazarın kitaplarının olup olmadığı kontrol edilir.
        /// </summary>
        /// <param name="id">Yazar ID</param>
        /// <exception cref="KeyNotFoundException">Yazar bulunamazsa</exception>
        /// <exception cref="InvalidOperationException">Yazarın kitapları varsa silinemez</exception>
        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Yazar bulunamadı ID: {id}");
            }

            // İlişkili kitaplar var mı kontrolü
            if (await _authorRepository.HasBooksAsync(id))
            {
                throw new InvalidOperationException("Bu yazarın kitapları bulunmaktadır. Önce ilişkili kitapları silmelisiniz.");
            }

            await _authorRepository.DeleteAsync(author);
        }
    }
}
