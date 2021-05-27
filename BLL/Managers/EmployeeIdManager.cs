using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    public class EmployeeIdManager : IEmployeeManager
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeIdManager(IRepository<Employee> repository, IMapper mapper)
        {
            _employeeRepository = repository;
            _mapper = mapper;
        }
        public EmployeeDto GetByUserId(int userId)
        {
            var items = _mapper.Map<ICollection<EmployeeDto>>(_employeeRepository.GetAll());
            var client = (from i in items
                          where i.UserId == userId
                          select i).FirstOrDefault();
            return client;
        }
    }
}
