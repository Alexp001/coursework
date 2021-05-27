using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;

namespace BLL.Managers
{
    class UserManager : IUserManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserManager(IRepository<User> repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }
        public void Update(UserDto user)
        {
            _userRepository.Update(_mapper.Map<User>(user));
        }
    }
}
