using System.Linq;
using AutoMapper;
using BookLibrary.BLL.DTOs;
using BookLibrary.DAL.Models.Entities;

namespace BookLibrary.BLL.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDTO, Author>()
                .ForMember(a => a.CreatedDate, opt => opt.Ignore())
                .ForMember(a => a.UpdatedDate, opt => opt.Ignore())
                .ForMember(a => a.AuthorBooks, opt => opt.Ignore());
            CreateMap<Author, AuthorDTO>()
                .ForMember(a => a.Books, opt => opt.MapFrom(x => x.AuthorBooks.Select(y => new BookDTO
                {
                    Id = y.Book.Id,
                    Name = y.Book.Name,
                    Description = y.Book.Description,
                    Year = y.Book.Year,
                    PagesCount = y.Book.PagesCount
                })));
        }
    }
}