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
            CreateMap<Author, AuthorDTO>();
        }
    }
}