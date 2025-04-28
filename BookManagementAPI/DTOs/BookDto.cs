using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    // Book için DTOs
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public AuthorSimpleDto Author { get; set; }
        public CategorySimpleDto Category { get; set; }
    }

    public class BookCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int PageCount { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }

    public class BookUpdateDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int PageCount { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }

    public class BookSimpleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
    }

}
