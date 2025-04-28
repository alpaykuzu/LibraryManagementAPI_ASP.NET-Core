using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagementAPI.Services.Implementations
{
    /// <summary>
    /// Yazar işlemleri için servis implementasyonu
    /// </summary>
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsWithBooksAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAuthorWithBooksAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Yazar bulunamadı ID: {id}");
            }
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> CreateAuthorAsync(AuthorCreateDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            var createdAuthor = await _authorRepository.AddAsync(author);

            // Eklenen yazarı detaylarıyla birlikte almak için
            var authorWithDetails = await _authorRepository.GetAuthorWithBooksAsync(createdAuthor.Id);
            return _mapper.Map<AuthorDto>(authorWithDetails);
        }

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

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Yazar bulunamadı ID: {id}");
            }

            // İlişkili kitapları kontrol et
            if (await _authorRepository.HasBooksAsync(id))
            {
                throw new InvalidOperationException("Bu yazarın kitapları bulunmaktadır. Önce ilişkili kitapları silmelisiniz.");
            }

            await _authorRepository.DeleteAsync(author);
        }
    }
}