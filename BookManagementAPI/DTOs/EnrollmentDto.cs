using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    // Enrollment için DTOs
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public StudentSimpleDto Student { get; set; }
        public BookSimpleDto Book { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }

    public class EnrollmentCreateDto
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int BookId { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.Now;

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; } = false;
    }

    public class EnrollmentUpdateDto
    {
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }

    public class EnrollmentSimpleDto
    {
        public int Id { get; set; }
        public BookSimpleDto Book { get; set; }
        public DateTime BorrowDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
