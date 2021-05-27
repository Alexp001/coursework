using AutoMapper;
using BLL.DTO;
using PL.Models;

namespace PL.Mapper
{
    public class PlMapper : Profile
    {
        public PlMapper()
        {
            CreateMap<ClientDto, ClientViewModel>().ReverseMap();
            CreateMap<CommentDto, CommentViewModel>().ReverseMap();
            CreateMap<OrderItemDto, OrderItemViewModel>().ReverseMap();
            CreateMap<PizzaDto, PizzaViewModel>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeViewModel>().ReverseMap();
            CreateMap<OrderDto, OrderViewModel>().ReverseMap();
            CreateMap<UserDto, UserViewModel>().ReverseMap();
            CreateMap<RoleDto, RoleViewModel>().ReverseMap();
            CreateMap<ClientUserDto, ClientUserViewModel>().ReverseMap();
            CreateMap<UserRoleDto, UserRoleViewModel>().ReverseMap();
            CreateMap<PizzaAccountingDto, PizzaAccountingViewModel>().ReverseMap();
            CreateMap<EmployeeUserDto, EmployeeUserViewModel>().ReverseMap();
            CreateMap<ReportByEmployeeCountDto, ReportByEmployeeCountViewModel>().ReverseMap();
            CreateMap<ReportByEmployeePriceDto, ReportByEmployeePriceViewModel>().ReverseMap();
            CreateMap<ReportByPizzaCountDto, ReportByPizzaCountViewModel>().ReverseMap();
            CreateMap<ReportByPizzaPriceDto, ReportByPizzaPriceViewModel>().ReverseMap();
            CreateMap<ReportByClientDto, ReportByClientViewModel>().ReverseMap();

        }
    }
}