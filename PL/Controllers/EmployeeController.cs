using AutoMapper;
using BLL.DTO;
using BLL.Managers;
using PL.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IManager<EmployeeUserDto> _employeeUserManager;
        private readonly IManager<EmployeeDto> _employeeManager;
        private readonly IRoleManager _roleManager;
        private readonly IManager<UserRoleDto> _userManager;
        private readonly IMapper _mapper;
        public EmployeeController(IManager<EmployeeUserDto> manager, IManager<EmployeeDto> manager2,
            IRoleManager manager1, IManager<UserRoleDto> manager3, IMapper mapper)
        {
            _employeeUserManager = manager;
            _employeeManager = manager2;
            _roleManager = manager1;
            _userManager = manager3;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var items = _mapper.Map<ICollection<EmployeeUserViewModel>>(_employeeUserManager.GetAll()); 
            return View(items);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeUserViewModel employee, bool[] roles)
        {
            var items = _mapper.Map<ICollection<RoleViewModel>>(_roleManager.GetAll());
            int i = 0;
            foreach (var item in items)
            {
                if (item.Name == "user")
                    continue;
                if (roles[i])
                {
                    employee.Roles.Add(item);
                    i += 2;
                }
                else
                {
                    i++;
                }
            }
            if (IsValid(ModelState))
                return View();
            if (roles.Length == 7)
                return View("Error", new ErrorViewModel { Message = "No choosen roles", ViewName = "Create", ControllerName = "Employee" });

            try
            {
                _employeeUserManager.Create(_mapper.Map<EmployeeUserDto>(employee));
            }
            catch
            {
                return View("Error", new ErrorViewModel { Message = "This login already exists", ViewName = "Index", ControllerName = "Login" });
            }
            return RedirectToAction("Index", "Employee", null);
        }
        [HttpGet]
        public ActionResult Update(int? id)
        {

            EmployeeUserViewModel employee = _mapper.Map<EmployeeUserViewModel>(_employeeUserManager.GetById(id.Value));
            return View(employee);
        }
        [HttpPost]
        public ActionResult Update(EmployeeUserViewModel employee, bool[] roles)
        {
            var items = _mapper.Map<ICollection<RoleViewModel>>(_roleManager.GetAll());
            int i = 0;
            employee.Roles = new List<RoleViewModel>();
            foreach (var item in items)
            {
                if (item.Name == "user")
                    continue;
                if (roles[i])
                {
                    employee.Roles.Add(item);
                    i += 2;
                }
                else
                {
                    i++;
                }
            }
            if (IsValid(ModelState))
                return View(employee);
            if (roles.Length == 7)
                return View("Error", new ErrorViewModel { Message = "No choosen roles", ViewName = "Create", ControllerName = "Employee" });
            _employeeUserManager.Update(_mapper.Map<EmployeeUserDto>(employee));
            return RedirectToAction("Index", "Employee", null);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            EmployeeViewModel employee = _mapper.Map<EmployeeViewModel>(_employeeManager.GetById(id.Value));
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(EmployeeViewModel employee)
        {
            _employeeUserManager.Delete(employee.Id);
            _userManager.Delete(employee.UserId);
            return RedirectToAction("Index", "Employee", null);
        }
        private bool IsValid(ModelStateDictionary keys)
        {
            foreach (var item in keys)
            {
                if (item.Key != "Roles" && item.Value.Errors.Count != 0)
                    return true;
            }
            return false;
        }
    }
}