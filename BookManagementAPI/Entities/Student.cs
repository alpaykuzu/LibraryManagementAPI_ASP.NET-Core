using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookManagementAPI.Entities
{
    /// <summary>
    /// Öğrenci entity sınıfı
    /// </summary>
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string StudentNumber { get; set; }

        // Many-to-Many ilişki: Bir öğrenci birden fazla kitap ödünç alabilir
        [JsonIgnore] // Sonsuz döngüyü engellemek için
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
