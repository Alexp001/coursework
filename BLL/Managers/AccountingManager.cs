using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    internal class AccountingManager : IManager<AccountingDto>
    {
        private readonly IRepository<Accounting> _accountingRepository;
        private readonly IMapper _mapper;
        public AccountingManager(IRepository<Accounting> repository, IMapper mapper)
        {
            _accountingRepository = repository;
            _mapper = mapper;
        }
        public ICollection<AccountingDto> GetAll()
        {
            return _mapper.Map<IEnumerable<AccountingDto>>(_accountingRepository.GetAll()).ToList();
        }
        public AccountingDto GetById(int id)
        {
            return _mapper.Map<AccountingDto>(_accountingRepository.GetById(id));
        }
        public int Create(AccountingDto accounting)
        {
            return _accountingRepository.Create(_mapper.Map<Accounting>(accounting));
        }
        public void Update(AccountingDto accounting)
        {
            _accountingRepository.Update(_mapper.Map<Accounting>(accounting));
        }
        public void Delete(int id)
        {
            _accountingRepository.Delete(id);
        }
    }
}
