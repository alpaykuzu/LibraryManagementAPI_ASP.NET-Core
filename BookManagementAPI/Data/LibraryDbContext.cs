using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Entities;

namespace BookManagementAPI.Data
{
    /// <summary>
    /// Veritabanı bağlantısı ve model yapılandırması için DbContext sınıfı
    /// </summary>
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        /// <summary>
        /// Entity ilişkilerini ve kurallarını yapılandırma
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Author ve Book arasındaki One-to-Many ilişki
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict); // Yazar silindiğinde kitapları silinmez

            // Category ve Book arasındaki One-to-Many ilişki
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Kategori silindiğinde kitapları silinmez

            // Student ve Book arasındaki Many-to-Many ilişki (Enrollment üzerinden)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Book)
                .WithMany(b => b.Enrollments)
                .HasForeignKey(e => e.BookId);

            // Temel seed data ekleme
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Başlangıç test verileri ekleme
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "George", Surname = "Orwell", Biography = "English novelist and essayist." },
                new Author { Id = 2, Name = "J.K.", Surname = "Rowling", Biography = "British author and philanthropist." }
            );

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Science Fiction", Description = "Books that depict imagined future scientific advances and developments." },
                new Category { Id = 2, Name = "Fantasy", Description = "Books with magic, supernatural elements, or other extraordinary circumstances." }
            );

            // Books
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

            // Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "John", Surname = "Doe", Email = "john.doe@example.com", StudentNumber = "ST001" },
                new Student { Id = 2, Name = "Jane", Surname = "Smith", Email = "jane.smith@example.com", StudentNumber = "ST002" }
            );

            // Enrollments
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    Id = 1,
                    StudentId = 1,
                    BookId = 1,
                    BorrowDate = new DateTime(2025, 4, 17), // Sabit tarih
                    ReturnDate = null,
                    IsReturned = false
                },
                new Enrollment
                {
                    Id = 2,
                    StudentId = 2,
                    BookId = 2,
                    BorrowDate = new DateTime(2025, 4, 22), // Sabit tarih
                    ReturnDate = new DateTime(2025, 4, 25), // Sabit tarih
                    IsReturned = true
                }
            );
        }
    }
}
