using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    /// <summary>
    /// Kitap bilgilerini temsil eden DTO sınıfı.
    /// Bu sınıf, kitapla ilgili temel bilgileri içerir, yazar ve kategori bilgilerini de içerir.
    /// </summary>
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public AuthorSimpleDto Author { get; set; }
        public CategorySimpleDto Category { get; set; }
    }

    /// <summary>
    /// Yeni bir kitap oluşturmak için gereken DTO sınıfı.
    /// </summary>
    public class BookCreateDto
    {
        [Required] // Başlık zorunlu
        [StringLength(200)] // Maksimum uzunluk 200 karakter
        public string Title { get; set; }

        [StringLength(500)] // Maksimum uzunluk 500 karakter
        public string Description { get; set; }
        public int PageCount { get; set; }

        [StringLength(20)] // Maksimum uzunluk 20 karakter
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }

        [Required] // Yazar ID zorunlu
        public int AuthorId { get; set; }

        [Required] // Kategori ID zorunlu
        public int CategoryId { get; set; }
    }

    /// <summary>
    /// Var olan bir kitabı güncellemek için gereken DTO sınıfı.
    /// </summary>
    public class BookUpdateDto
    {
        [Required] // Başlık zorunlu
        [StringLength(200)] // Maksimum uzunluk 200 karakter
        public string Title { get; set; }

        [StringLength(500)] // Maksimum uzunluk 500 karakter
        public string Description { get; set; }
        public int PageCount { get; set; }

        [StringLength(20)] // Maksimum uzunluk 20 karakter
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }

        [Required] // Yazar ID zorunlu
        public int AuthorId { get; set; }

        [Required] // Kategori ID zorunlu
        public int CategoryId { get; set; }
    }

    /// <summary>
    /// Kitabın temel bilgilerini temsil eden DTO sınıfı.
    /// Bu sınıf, yalnızca kitabın kimliği, başlığı ve ISBN numarasını içerir.
    /// </summary>
    public class BookSimpleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
    }
}
