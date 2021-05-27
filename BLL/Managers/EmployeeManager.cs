using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    class EmployeeManager : IManager<EmployeeDto>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeManager(IRepository<Employee> repository, IMapper mapper)
        {
            _employeeRepository = repository;
            _mapper = mapper;
        }
        public ICollection<EmployeeDto> GetAll()
        {
            return _mapper.Map<IEnumerable<EmployeeDto>>(_employeeRepository.GetAll()).ToList();
        }
        public EmployeeDto GetById(int id)
        {
            return _mapper.Map<EmployeeDto>(_employeeRepository.GetById(id));
        }
        public int Create(EmployeeDto client)
        {
            return _employeeRepository.Create(_mapper.Map<Employee>(client));
        }
        public void Update(EmployeeDto client)
        {
            _employeeRepository.Update(_mapper.Map<Employee>(client));
        }
        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }
    }
}
