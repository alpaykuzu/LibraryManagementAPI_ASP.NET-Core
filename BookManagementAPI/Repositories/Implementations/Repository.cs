using BookManagementAPI.Data;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagementAPI.Repositories.Implementations
{
    /// <summary>
    /// Generic repository implementasyonu. Tüm temel veritabanı işlemleri için kullanılır.
    /// </summary>
    /// <typeparam name="T">Veri modeli tipi (Entity)</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Veritabanı context'i
        /// </summary>
        protected readonly LibraryDbContext _context;

        /// <summary>
        /// İlgili entity'e ait DbSet
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Repository constructor'ı. Context ve ilgili DbSet tanımlanır.
        /// </summary>
        /// <param name="context">Veritabanı context'i</param>
        public Repository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Tüm kayıtları getirir.
        /// </summary>
        /// <returns>Entity listesi</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kaydı getirir.
        /// </summary>
        /// <param name="id">Kayıt ID'si</param>
        /// <returns>İlgili entity veya null</returns>
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Yeni bir kayıt ekler.
        /// </summary>
        /// <param name="entity">Eklenecek entity</param>
        /// <returns>Eklenen entity</returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Var olan bir kaydı günceller.
        /// </summary>
        /// <param name="entity">Güncellenecek entity</param>
        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Bir kaydı siler.
        /// </summary>
        /// <param name="entity">Silinecek entity</param>
        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kaydın veritabanında olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="id">Kayıt ID'si</param>
        /// <returns>Var ise true, yok ise false</returns>
        public virtual async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }
    }
}
