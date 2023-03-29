using AutoMapper;
using BookLibrary.Domain.Core.DTO.BookDTOs;
using BookLibrary.Domain.Core.DTO.OrderDTOs;
using BookLibrary.Domain.Core.DTO.UserDTOs;
using BookLibrary.Domain.Core.Models;

namespace BookLibrary
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookView>();
            CreateMap<Book, BookDto>();
            CreateMap<AddBookDto, Book>();
            CreateMap<EditBookDto, Book>();

            CreateMap<Order, OrderView>();
            CreateMap<Order, OrderDto>();
            CreateMap<AddOrderDto, Order>();

            CreateMap<ApplicationUser, UserView>();
            CreateMap<ApplicationUser, GetUserDto>();
            CreateMap<UpdateUserDto, ApplicationUser>();
        }
    }
}
