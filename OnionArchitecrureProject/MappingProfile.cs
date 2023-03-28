using AutoMapper;
using DomainLayer.DTO.BookDtos;
using DomainLayer.DTO.OrderDtos;
using DomainLayer.DTO.UserDtos;
using DomainLayer.Models;

namespace WebAPI_Layer
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
