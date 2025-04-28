using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Entities;

namespace BookManagementAPI.Data
{
    /// <summary>
    /// Veritabanı bağlantısı ve model yapılandırması için DbContext sınıfı.
    /// Bu sınıf, tüm entity sınıflarınızla ilişkili DbSet'leri içerir ve 
    /// veritabanı yapılandırması ile seed data eklemeyi yönetir.
    /// </summary>
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        // DbSet'ler: Veritabanında yer alan tüm tablolar
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        /// <summary>
        /// Entity ilişkilerini ve kurallarını yapılandırmak için bu metot kullanılır.
        /// Veritabanı tablolarındaki ilişkiler ve silme davranışları burada tanımlanır.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder nesnesi, entity'ler arasındaki ilişkileri tanımlar.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Yazar ve Kitap arasındaki ilişki: Bir yazarın birden fazla kitabı olabilir.
            // Yazar silindiğinde kitaplar silinmez (DeleteBehavior.Restrict).
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Kategori ve Kitap arasındaki ilişki: Bir kategori birden fazla kitaba sahip olabilir.
            // Kategori silindiğinde kitaplar silinmez.
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Öğrenci ve Kitap arasındaki Many-to-Many ilişki: Öğrenci, kitapları ödünç alabilir (Enrollments tablosu üzerinden).
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Book)
                .WithMany(b => b.Enrollments)
                .HasForeignKey(e => e.BookId);

            // Test verileri eklemek için seed data kullanımı
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Başlangıç test verilerini veritabanına eklemek için kullanılan yardımcı metod.
        /// Bu metod, veritabanı ilk başlatıldığında örnek veri sağlar.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder nesnesi, seed data ekler.</param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Yazarlar
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "George", Surname = "Orwell", Biography = "English novelist and essayist." },
                new Author { Id = 2, Name = "J.K.", Surname = "Rowling", Biography = "British author and philanthropist." }
            );

            // Kategoriler
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Science Fiction", Description = "Books that depict imagined future scientific advances and developments." },
                new Category { Id = 2, Name = "Fantasy", Description = "Books with magic, supernatural elements, or other extraordinary circumstances." }
            );

            // Kitaplar
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "1984",
                    Description = "A dystopian social science fiction novel.",
                    PageCount = 328,
                    ISBN = "978-0451524935",
                    PublicationDate = new DateTime(1949, 6, 8),
                    AuthorId = 1,
                    CategoryId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "The first novel in the Harry Potter series.",
                    PageCount = 223,
                    ISBN = "978-0747532699",
                    PublicationDate = new DateTime(1997, 6, 26),
                    AuthorId = 2,
                    CategoryId = 2
                }
            );

            // Öğrenciler
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "John", Surname = "Doe", Email = "john.doe@example.com", StudentNumber = "ST001" },
                new Student { Id = 2, Name = "Jane", Surname = "Smith", Email = "jane.smith@example.com", StudentNumber = "ST002" }
            );

            // Ödünç alma kayıtları (Enrollments)
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    Id = 1,
                    StudentId = 1,
                    BookId = 1,
                    BorrowDate = new DateTime(2025, 4, 17), // Sabit tarih (ödünç alma tarihi)
                    ReturnDate = null,
                    IsReturned = false
                },
                new Enrollment
                {
                    Id = 2,
                    StudentId = 2,
                    BookId = 2,
                    BorrowDate = new DateTime(2025, 4, 22), // Sabit tarih (ödünç alma tarihi)
                    ReturnDate = new DateTime(2025, 4, 25), // Sabit tarih (kitap iade tarihi)
                    IsReturned = true
                }
            );
        }
    }
}
