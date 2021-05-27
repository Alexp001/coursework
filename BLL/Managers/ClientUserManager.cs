using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    class ClientUserManager : IManager<ClientUserDto>
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<AccountingRoles> _accountingRepository;
        private readonly IMapper _mapper;
        public ClientUserManager(IRepository<Client> clientRepository, IRepository<User> userRepository, IRepository<Role> roleRepository,
            IRepository<AccountingRoles> accountingRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _accountingRepository = accountingRepository;
            _mapper = mapper;
        }
        public ICollection<ClientUserDto> GetAll()
        {
            var clients = _mapper.Map<ICollection<ClientDto>>(_clientRepository.GetAll());
            var users = _mapper.Map<ICollection<UserDto>>(_userRepository.GetAll());
            var roles = _mapper.Map<ICollection<RoleDto>>(_roleRepository.GetAll());
            var accountings = _mapper.Map<ICollection<AccountingRolesDto>>(_accountingRepository.GetAll());

            var groupRoles = from r in roles
                              join a in accountings on r.Id equals a.RoleId
                              group r by a.UserId into g
                              select new { g.Key, roles = from p in g select p };
            var quary = (from c in clients
                         join u in users on c.UserId equals u.Id
                         join r in groupRoles on u.Id equals r.Key
                         select new ClientUserDto { ClientObject = c, User = u, Roles = r.roles }).ToList();

            return quary;
        }
        public ClientUserDto GetById(int id)
        {
            return null;
        }
        public int Create(ClientUserDto client)
        {
            client.ClientObject.UserId = _userRepository.Create(_mapper.Map<User>(client.User));
            _clientRepository.Create(_mapper.Map<Client>(client.ClientObject));
            AccountingRolesDto rolesDto;
            foreach(var role in client.Roles)
            {
                rolesDto = new AccountingRolesDto() { UserId = client.ClientObject.UserId, RoleId  = role.Id };
                _accountingRepository.Create(_mapper.Map<AccountingRoles>(rolesDto));
            }
            return client.ClientObject.UserId;
        }
        public void Update(ClientUserDto client)
        {
        }
        public void Delete(int id)
        {
            _clientRepository.Delete(id);
        }
    }
}
