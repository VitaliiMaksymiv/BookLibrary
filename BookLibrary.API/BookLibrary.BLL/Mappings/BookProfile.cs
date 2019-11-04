using System.Linq;
using AutoMapper;
using BookLibrary.BLL.DTOs;
using BookLibrary.DAL.Models.Entities;

namespace BookLibrary.BLL.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDTO, Book>()
                .ForMember(a => a.CreatedDate, opt => opt.Ignore())
                .ForMember(a => a.UpdatedDate, opt => opt.Ignore())
                .ForMember(a => a.AuthorBooks, opt => opt.Ignore());
            CreateMap<Book, BookDTO>()
                .ForMember(a => a.Authors,
                    opt => opt.MapFrom(x => x.AuthorBooks.Select(y => new AuthorDTO
                    {
                        Id = y.Author.Id,
                        Name = y.Author.Name,
                        Year = y.Author.Year
                    })));
        }
    }
}