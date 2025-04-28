using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookManagementAPI.Entities
{
    /// <summary>
    /// Yazar entity sınıfı
    /// </summary>
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(500)]
        public string Biography { get; set; }

        // One-to-Many ilişki: Bir yazar birden fazla kitap yazabilir
        [JsonIgnore] // Sonsuz döngüyü engellemek için
        public virtual ICollection<Book> Books { get; set; }
    }
}
