using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    /// <summary>
    /// Öğrenci ve kitap ödünç alma bilgilerini temsil eden DTO sınıfı.
    /// Bu sınıf, ödünç alınan kitap, öğrenci ve ilgili ödünç alma tarih bilgilerini içerir.
    /// </summary>
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public StudentSimpleDto Student { get; set; }
        public BookSimpleDto Book { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }

    /// <summary>
    /// Yeni bir ödünç alma kaydı oluşturmak için gereken DTO sınıfı.
    /// </summary>
    public class EnrollmentCreateDto
    {
        [Required] // Öğrenci kimliği zorunlu
        public int StudentId { get; set; }

        [Required] // Kitap kimliği zorunlu
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
    }

    /// <summary>
    /// Var olan bir ödünç alma kaydını güncellemek için gereken DTO sınıfı.
    /// </summary>
    public class EnrollmentUpdateDto
    {
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }

    /// <summary>
    /// Öğrenci ve ödünç alınan kitabın temel bilgilerini temsil eden DTO sınıfı.
    /// </summary>
    public class EnrollmentSimpleDto
    {
        public int Id { get; set; }
        public BookSimpleDto Book { get; set; }
        public DateTime BorrowDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
