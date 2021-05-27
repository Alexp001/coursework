using AutoMapper;
using BLL.DTO;
using BLL.Managers;
using PL.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Text.Json;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        private readonly IManager<UserRoleDto> _userManager;
        private readonly IMapper _mapper;
        public LoginController(IManager<UserRoleDto> manager, IMapper mapper)
        {
            _userManager = manager;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var users = _mapper.Map<ICollection<UserRoleViewModel>>(_userManager.GetAll());
            UserRoleViewModel userRole = new UserRoleViewModel();
            foreach (var item in users)
            {
                if (item.User.Login.Equals(user.Login))
                {
                    if (item.User.Password.Equals(user.Password))
                    {
                        userRole.User = item.User;
                        userRole.Roles = item.Roles;
                        HttpContext.Response.Cookies["user"].Value = JsonSerializer.Serialize<UserRoleViewModel>(userRole);

                        return RedirectToAction("Index", "Home", null);
                    }
                }
            }
            return View("Error", new ErrorViewModel { Message = "Invalid login or password", ViewName = "Index", ControllerName = "Login" });
        }

    }
}