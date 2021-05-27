using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        public RoleManager(IRepository<Role> repository, IMapper mapper)
        {
            _roleRepository = repository;
            _mapper = mapper;
        }
        public ICollection<RoleDto> GetAll()
        {
            return _mapper.Map<ICollection<RoleDto>>(_roleRepository.GetAll()).ToList();
        }
    }
}
