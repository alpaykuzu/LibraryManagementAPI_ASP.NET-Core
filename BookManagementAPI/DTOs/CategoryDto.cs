using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    /// <summary>
    /// Kategori bilgilerini temsil eden DTO sınıfı.
    /// Bu sınıf, kategorinin temel bilgilerini içerir, aynı zamanda o kategoriye ait kitaplar da yer alır.
    /// </summary>
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BookSimpleDto> Books { get; set; }
    }

    /// <summary>
    /// Yeni bir kategori oluşturmak için gereken DTO sınıfı.
    /// </summary>
    public class CategoryCreateDto
    {
        [Required] // Kategori adı zorunlu
        [StringLength(100)] // Maksimum uzunluk 100 karakter
        public string Name { get; set; }

        [StringLength(500)] // Maksimum uzunluk 500 karakter
        public string Description { get; set; }
    }

    /// <summary>
    /// Var olan bir kategoriyi güncellemek için gereken DTO sınıfı.
    /// </summary>
    public class CategoryUpdateDto
    {
        [Required] // Kategori adı zorunlu
        [StringLength(100)] // Maksimum uzunluk 100 karakter
        public string Name { get; set; }

        [StringLength(500)] // Maksimum uzunluk 500 karakter
        public string Description { get; set; }
    }

    /// <summary>
    /// Kategorinin temel bilgilerini temsil eden DTO sınıfı.
    /// Bu sınıf, yalnızca kategorinin kimliği ve adını içerir.
    /// </summary>
    public class CategorySimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
