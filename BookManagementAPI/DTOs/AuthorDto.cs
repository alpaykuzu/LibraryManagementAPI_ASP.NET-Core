using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    // Author için DTOs
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Biography { get; set; }
        public List<BookSimpleDto> Books { get; set; }
    }

    public class AuthorCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(500)]
        public string Biography { get; set; }
    }

    public class AuthorUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(500)]
        public string Biography { get; set; }
    }

    public class AuthorSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
