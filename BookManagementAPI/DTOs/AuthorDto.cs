using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    /// <summary>
    /// Yazar bilgilerini temsil eden DTO sınıfı.
    /// Bu sınıf, yazarla ilgili temel bilgileri içerir.
    /// </summary>
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Biography { get; set; }
        public List<BookSimpleDto> Books { get; set; }
    }

    /// <summary>
    /// Yeni bir yazar oluşturmak için gereken DTO sınıfı.
    /// </summary>
    public class AuthorCreateDto
    {
        [Required] // Ad zorunludur
        [StringLength(100)] // Maksimum uzunluk 100 karakter
        public string Name { get; set; }

        [StringLength(100)] // Maksimum uzunluk 100 karakter
        public string Surname { get; set; }

        [StringLength(500)] // Maksimum uzunluk 500 karakter
        public string Biography { get; set; }
    }

    /// <summary>
    /// Var olan bir yazarın bilgilerini güncellemek için gereken DTO sınıfı.
    /// </summary>
    public class AuthorUpdateDto
    {
        [Required] // Ad zorunludur
        [StringLength(100)] // Maksimum uzunluk 100 karakter
        public string Name { get; set; }

        [StringLength(100)] // Maksimum uzunluk 100 karakter
        public string Surname { get; set; }

        [StringLength(500)] // Maksimum uzunluk 500 karakter
        public string Biography { get; set; }
    }

    /// <summary>
    /// Yazarın temel bilgilerini temsil eden DTO sınıfı.
    /// Bu sınıf, yalnızca yazarın kimliği, adı ve soyadını içerir.
    /// </summary>
    public class AuthorSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
