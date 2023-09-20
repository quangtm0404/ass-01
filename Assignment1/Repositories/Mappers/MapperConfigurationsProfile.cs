using AutoMapper;
using Domains.Entities;
using Services.ViewModels;
using Services.ViewModels.OrderDetailDTO;
using Services.ViewModels.OrderDTO;
using Services.ViewModels.ProductDTO;

namespace Repositories.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();


            CreateMap<Member, MemberDTO>().ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id)).ReverseMap();
            CreateMap<Member, CreateMemberDTO>().ReverseMap();
            CreateMap<Member, LoginMemberDTO>().ReverseMap();
            CreateMap<Member, UpdateMemberDTO>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailCreateDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewDTO>().ReverseMap();

            CreateMap<Order, OrderViewDTO>()
                .ReverseMap();

            CreateMap<Order, OrderCreateDTO>()
                .ReverseMap()
                .ForMember(x => x.OrderDetails, opt => opt.MapFrom(x => x.OrderDetails));
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();
        }
    }
}
