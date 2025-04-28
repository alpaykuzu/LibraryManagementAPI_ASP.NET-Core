using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    // Category için DTOs
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BookSimpleDto> Books { get; set; }
    }

    public class CategoryCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }

    public class CategoryUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }

    public class CategorySimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
