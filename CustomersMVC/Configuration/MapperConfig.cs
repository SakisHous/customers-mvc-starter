using AutoMapper;
using CustomersMVC.Data;
using CustomersMVC.DTO;
using CustomersMVC.Models;

namespace CustomersMVC.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Customer, CustomerSignupDTO>().ReverseMap();
            CreateMap<ProductReadOnlyDTO, Product>().ReverseMap();
            CreateMap<OrderLine, OrderReadOnlyDTO>().ReverseMap();
        }
    }
}
