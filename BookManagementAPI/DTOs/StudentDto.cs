using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    // Student için DTOs
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string StudentNumber { get; set; }
        public List<EnrollmentSimpleDto> Enrollments { get; set; }
    }

    public class StudentCreateDto
    {
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
    }

    public class StudentUpdateDto
    {
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
    }

    public class StudentSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
    }

}
