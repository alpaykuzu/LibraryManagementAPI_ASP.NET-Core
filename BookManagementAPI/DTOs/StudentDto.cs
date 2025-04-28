using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    /// <summary>
    /// Öğrenci bilgilerini temsil eden DTO sınıfı.
    /// Bu sınıf, öğrenci hakkında temel bilgileri ve öğrencinin ödünç alma işlemlerine ait verileri içerir.
    /// </summary>
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string StudentNumber { get; set; }
        public List<EnrollmentSimpleDto> Enrollments { get; set; }
    }

    /// <summary>
    /// Yeni bir öğrenci oluşturmak için gereken DTO sınıfı.
    /// </summary>
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

    /// <summary>
    /// Var olan bir öğrenci kaydını güncellemek için gereken DTO sınıfı.
    /// </summary>
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

    /// <summary>
    /// Öğrencinin temel bilgilerini temsil eden DTO sınıfı (daha basit versiyon).
    /// </summary>
    public class StudentSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
    }
}
