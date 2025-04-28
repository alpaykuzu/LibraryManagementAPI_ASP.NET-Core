using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookManagementAPI.Entities
{
    /// <summary>
    /// Kitap entity sınıfı
    /// </summary>
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int PageCount { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        public DateTime PublicationDate { get; set; }

        // One-to-Many ilişki: Bir kitap bir yazara ait olabilir
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        // One-to-Many ilişki: Bir kitap bir kategoriye ait olabilir
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        // Many-to-Many ilişki: Bir kitap birden fazla öğrenci tarafından ödünç alınabilir
        [JsonIgnore] // Sonsuz döngüyü engellemek için
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
