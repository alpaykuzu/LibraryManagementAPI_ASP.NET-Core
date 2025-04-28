using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Entities
{
    /// <summary>
    /// Ödünç alma/kaydolma işlemi entity sınıfı (Many-to-Many ilişkiyi sağlayan ara tablo)
    /// </summary>
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        // Many-to-Many ilişki: Öğrenci ve Kitap arasında
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; }
    }
}
