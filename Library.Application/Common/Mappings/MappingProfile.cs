using AutoMapper;
using Library.Application.Books.Dtos;
using Library.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Application.Common.Mappings
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
          .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
          .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
          .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.ToString()))
          .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn != null ? src.Isbn.Value : string.Empty))
          .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author.FullName).ToList()));
        }
    }
    
}
