using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Entities;

namespace BookManagementAPI.Mapping
{
    /// <summary>
    /// Entity ve DTO'lar arasında dönüşüm işlemlerini yöneten AutoMapper profili.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // -------------------------------
            // Author (Yazar) mapping'leri
            // -------------------------------

            // Author -> AuthorDto dönüşümü (kitap bilgileriyle birlikte)
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            // AuthorCreateDto -> Author dönüşümü (yeni yazar ekleme)
            CreateMap<AuthorCreateDto, Author>();

            // AuthorUpdateDto -> Author dönüşümü (mevcut yazarı güncelleme)
            CreateMap<AuthorUpdateDto, Author>();

            // Author -> AuthorSimpleDto dönüşümü (yalnızca temel bilgiler)
            CreateMap<Author, AuthorSimpleDto>();

            // -------------------------------
            // Category (Kategori) mapping'leri
            // -------------------------------

            // Category -> CategoryDto dönüşümü (kitap bilgileriyle birlikte)
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            // CategoryCreateDto -> Category dönüşümü (yeni kategori ekleme)
            CreateMap<CategoryCreateDto, Category>();

            // CategoryUpdateDto -> Category dönüşümü (mevcut kategoriyi güncelleme)
            CreateMap<CategoryUpdateDto, Category>();

            // Category -> CategorySimpleDto dönüşümü (yalnızca temel bilgiler)
            CreateMap<Category, CategorySimpleDto>();

            // -------------------------------
            // Book (Kitap) mapping'leri
            // -------------------------------

            // Book -> BookDto dönüşümü (yazar ve kategori bilgileriyle birlikte)
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            // BookCreateDto -> Book dönüşümü (yeni kitap ekleme)
            CreateMap<BookCreateDto, Book>();

            // BookUpdateDto -> Book dönüşümü (mevcut kitabı güncelleme)
            CreateMap<BookUpdateDto, Book>();

            // Book -> BookSimpleDto dönüşümü (yalnızca temel bilgiler)
            CreateMap<Book, BookSimpleDto>();

            // -------------------------------
            // Student (Öğrenci) mapping'leri
            // -------------------------------

            // Student -> StudentDto dönüşümü (kayıt bilgileriyle birlikte)
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments));

            // StudentCreateDto -> Student dönüşümü (yeni öğrenci ekleme)
            CreateMap<StudentCreateDto, Student>();

            // StudentUpdateDto -> Student dönüşümü (mevcut öğrenciyi güncelleme)
            CreateMap<StudentUpdateDto, Student>();

            // Student -> StudentSimpleDto dönüşümü (yalnızca temel bilgiler)
            CreateMap<Student, StudentSimpleDto>();

            // -------------------------------
            // Enrollment (Kayıt) mapping'leri
            // -------------------------------

            // Enrollment -> EnrollmentDto dönüşümü (öğrenci ve kitap bilgileriyle birlikte)
            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student))
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

            // EnrollmentCreateDto -> Enrollment dönüşümü (yeni kayıt oluşturma)
            CreateMap<EnrollmentCreateDto, Enrollment>();

            // EnrollmentUpdateDto -> Enrollment dönüşümü (mevcut kaydı güncelleme)
            // Sadece null olmayan değerler güncellenir.
            CreateMap<EnrollmentUpdateDto, Enrollment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Enrollment -> EnrollmentSimpleDto dönüşümü (yalnızca kitap bilgisi ile temel kayıt bilgisi)
            CreateMap<Enrollment, EnrollmentSimpleDto>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
        }
    }
}
