using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    public class EmployeeUserManager : IManager<EmployeeUserDto>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<AccountingRoles> _accountingRepository;
        private readonly IMapper _mapper;
        public EmployeeUserManager(IRepository<Employee> employeeRepository, IRepository<User> userRepository, IRepository<Role> roleRepository,
            IRepository<AccountingRoles> accountingRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _accountingRepository = accountingRepository;
            _mapper = mapper;
        }
        public ICollection<EmployeeUserDto> GetAll()
        {
            var employees = _mapper.Map<ICollection<EmployeeDto>>(_employeeRepository.GetAll());
            var users = _mapper.Map<ICollection<UserDto>>(_userRepository.GetAll());
            var roles = _mapper.Map<ICollection<RoleDto>>(_roleRepository.GetAll());
            var accountings = _mapper.Map<ICollection<AccountingRolesDto>>(_accountingRepository.GetAll());

            var groupRoles = from r in roles
                             join a in accountings on r.Id equals a.RoleId
                             group r by a.UserId into g
                             select new { g.Key, roles = from p in g select p };
            var quary = (from e in employees
                         join u in users on e.UserId equals u.Id
                         join r in groupRoles on u.Id equals r.Key
                         select new EmployeeUserDto { EmployeeObject = e, User = u, Roles = r.roles }).ToList();

            return quary;
        }
        public EmployeeUserDto GetById(int id)
        {
            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetById(id));
            var users = _mapper.Map<ICollection<UserDto>>(_userRepository.GetAll());
            var roles = _mapper.Map<ICollection<RoleDto>>(_roleRepository.GetAll());
            var accountings = _mapper.Map<ICollection<AccountingRolesDto>>(_accountingRepository.GetAll());

            var groupRoles = from r in roles
                             join a in accountings on r.Id equals a.RoleId
                             group r by a.UserId into g
                             select new { g.Key, roles = from p in g select p };

            var quary = (from u in users
                         where u.Id == employee.UserId
                         join r in groupRoles on u.Id equals r.Key
                         select new EmployeeUserDto { EmployeeObject = employee, User = u, Roles = r.roles }).ToList();

            return quary.FirstOrDefault();
        }
        public int Create(EmployeeUserDto employee)
        {
            employee.EmployeeObject.UserId = _userRepository.Create(_mapper.Map<User>(employee.User));
            _employeeRepository.Create(_mapper.Map<Employee>(employee.EmployeeObject));
            AccountingRolesDto rolesDto;
            foreach (var role in employee.Roles)
            {
                rolesDto = new AccountingRolesDto() { UserId = employee.EmployeeObject.UserId, RoleId = role.Id };
                _accountingRepository.Create(_mapper.Map<AccountingRoles>(rolesDto));
            }
            return employee.EmployeeObject.UserId;
        }
        public void Update(EmployeeUserDto employee)
        {
            _employeeRepository.Update(_mapper.Map<Employee>(employee.EmployeeObject));
            var accounts = GetRolesByUserId(employee.User.Id);

            foreach (var role in accounts)
            {
                _accountingRepository.Delete(role.Id);
            }
            AccountingRolesDto rolesDto;
            foreach (var role in employee.Roles)
            {
                rolesDto = new AccountingRolesDto() { UserId = employee.User.Id, RoleId = role.Id };
                _accountingRepository.Create(_mapper.Map<AccountingRoles>(rolesDto));
            }
        }
        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }
        private List<AccountingRolesDto> GetRolesByUserId(int id)
        {
            var roles = _mapper.Map<ICollection<RoleDto>>(_roleRepository.GetAll());
            var accounts = _mapper.Map<ICollection<AccountingRolesDto>>(_accountingRepository.GetAll());

            var result = (from a in accounts
                         where a.UserId == id
                         select a).ToList();
            return result;
        }
    }
}
