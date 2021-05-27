using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    class UserRoleManager : IManager<UserRoleDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<AccountingRoles> _accountingRepository;
        private readonly IMapper _mapper;
        public UserRoleManager(IRepository<User> userRepository, IRepository<Role> roleRepository,
            IRepository<AccountingRoles> accountingRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _accountingRepository = accountingRepository;
            _mapper = mapper;
        }
        public ICollection<UserRoleDto> GetAll()
        {
            var users = _mapper.Map<ICollection<UserDto>>(_userRepository.GetAll());
            var roles = _mapper.Map<ICollection<RoleDto>>(_roleRepository.GetAll());
            var accountings = _mapper.Map<ICollection<AccountingRolesDto>>(_accountingRepository.GetAll());

            var groupRoles = from r in roles
                             join a in accountings on r.Id equals a.RoleId
                             group r by a.UserId into g
                             select new { g.Key, roles = from p in g select p };
            var quary = (from u in users
                        join r in groupRoles on u.Id equals r.Key
                        select new UserRoleDto { User = u, Roles = r.roles }).ToList();

            return quary;
        }
        public UserRoleDto GetById(int id)
        {
            return null;
        }
        public int Create(UserRoleDto user)
        {
            user.User.Id = _userRepository.Create(_mapper.Map<User>(user.User));

            AccountingRolesDto rolesDto;
            foreach (var role in user.Roles)
            {
                rolesDto = new AccountingRolesDto() { UserId = user.User.Id, RoleId = role.Id };
                _accountingRepository.Create(_mapper.Map<AccountingRoles>(rolesDto));
            }
            return user.User.Id;
        }
        public void Update(UserRoleDto client)
        {
        }
        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }
    }
}

