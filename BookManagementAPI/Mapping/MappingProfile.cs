using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;

namespace BookManagementAPI.Mapping
{
    /// <summary>
    /// Entity ve DTO'lar arasında dönüşüm için AutoMapper profili
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Author mappings
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
            CreateMap<Author, AuthorSimpleDto>();

            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategorySimpleDto>();

            // Book mappings
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();
            CreateMap<Book, BookSimpleDto>();

            // Student mappings
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments));
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<Student, StudentSimpleDto>();

            // Enrollment mappings
            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student))
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
            CreateMap<EnrollmentCreateDto, Enrollment>();
            CreateMap<EnrollmentUpdateDto, Enrollment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Enrollment, EnrollmentSimpleDto>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
        }
    }
}
