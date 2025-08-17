using AutoMapper;
using Library.Application.Books.Dtos;
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
                .ForMember(d => d.Isbn, opt => opt.MapFrom(s => s.Isbn.ToString()))
                .ForMember(d => d.Authors, opt => opt.MapFrom(s => s.BookAuthors.Select(ba => ba.Author.FullName)))
                .ForMember(d => d.Genre, opt => opt.MapFrom(s => s.Genre.ToString()));
        }
    }
}
