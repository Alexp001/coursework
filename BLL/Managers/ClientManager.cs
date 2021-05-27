using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    internal class ClientManager : IManager<ClientDto>
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IMapper _mapper;
        public ClientManager(IRepository<Client> repository, IMapper mapper)
        {
            _clientRepository = repository;
            _mapper = mapper;
        }
        public ICollection<ClientDto> GetAll()
        {
            return _mapper.Map<IEnumerable<ClientDto>>(_clientRepository.GetAll()).ToList();
        }
        public ClientDto GetById(int id)
        {
            return _mapper.Map<ClientDto>(_clientRepository.GetById(id));
        }
        public int Create(ClientDto client)
        {
            return _clientRepository.Create(_mapper.Map<Client>(client));
        }
        public void Update(ClientDto client)
        {
            _clientRepository.Update(_mapper.Map<Client>(client));
        }
        public void Delete(int id)
        {
            _clientRepository.Delete(id);
        }
    }
}
