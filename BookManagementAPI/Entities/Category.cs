using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookManagementAPI.Entities
{
    /// <summary>
    /// Kitap kategorisi entity sınıfı
    /// </summary>
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // One-to-Many ilişki: Bir kategori birden fazla kitap içerebilir
        [JsonIgnore] // Sonsuz döngüyü engellemek için
        public virtual ICollection<Book> Books { get; set; }
    }
}
