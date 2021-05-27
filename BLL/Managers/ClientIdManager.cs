using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    public class ClientIdManager : IClientManager
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IMapper _mapper;
        public ClientIdManager(IRepository<Client> repository, IMapper mapper)
        {
            _clientRepository = repository;
            _mapper = mapper;
        }
        public ClientDto GetByUserId(int userId)
        {
            var items = _mapper.Map<ICollection<ClientDto>>(_clientRepository.GetAll());
            var client = (from i in items
                         where i.UserId == userId
                         select i).FirstOrDefault();
            return client;
        }
    }
}
