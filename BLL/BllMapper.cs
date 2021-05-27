using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;

namespace BLL
{
    public class BllMapper : Profile
    {
        public BllMapper()
        {
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<AccountingDto, Accounting>().ReverseMap();
            CreateMap<CommentDto, Comment>().ReverseMap();
            CreateMap<OrderItemDto, Order>().ReverseMap();
            CreateMap<PizzaDto, Pizza>().ReverseMap();
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<RoleDto, Role>().ReverseMap();
            CreateMap<AccountingRolesDto, AccountingRoles>().ReverseMap();
        }
    }
}
