using AutoMapper;
using BLL.DTO;
using BLL.Managers;
using PL.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ClientController : Controller
    {
        private readonly IManager<ClientUserDto> _clientUserManager;
        private readonly IManager<ClientDto> _clientManager;
        private readonly IManager<UserRoleDto> _userManager;
        private readonly IMapper _mapper;
        public ClientController(IManager<ClientUserDto> manager, IManager<ClientDto> manager1, IManager<UserRoleDto> manager2, IMapper mapper)
        {
            _clientUserManager = manager;
            _clientManager = manager1;
            _userManager = manager2;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var items = _mapper.Map<ICollection<ClientUserViewModel>>(_clientUserManager.GetAll());

            return View(items);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            ClientViewModel client = _mapper.Map<ClientViewModel>(_clientManager.GetById(id.Value));

            return View(client);
        }
        [HttpPost]
        public ActionResult Update(ClientViewModel client)
        {
            if (!ModelState.IsValid)
                return View(client);
            _clientManager.Update(_mapper.Map<ClientDto>(client));

            return RedirectToAction("Index", "Client", null);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            ClientViewModel client = _mapper.Map<ClientViewModel>(_clientManager.GetById((int)id));
            return View(client);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(ClientViewModel client)
        {
            _clientManager.Delete(client.Id);
            _userManager.Delete(client.UserId);
            return RedirectToAction("Index", "Client", null);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ClientUserViewModel client)
        {
            if (!ModelState.IsValid)
                return View();
            var client1 = new List<RoleViewModel>
            {
                new RoleViewModel { Id = 1, Name = "user" }
            };
            client.Roles = client1;
            try
            {
                client.User.Id = _clientUserManager.Create(_mapper.Map<ClientUserDto>(client));
            }
            catch
            {
                return View("Error", new ErrorViewModel { Message = "This login already exists", ViewName = "Index", ControllerName = "Login" });
            }
            var users = _mapper.Map<ICollection<UserRoleViewModel>>(_userManager.GetAll());
            UserRoleViewModel userRole = new UserRoleViewModel();
            foreach (var item in users)
            {
                if (item.User.Login.Equals(client.User.Login))
                {
                    if (item.User.Password.Equals(client.User.Password))
                    {
                        userRole.User = item.User;
                        userRole.Roles = item.Roles;
                        HttpContext.Response.Cookies["user"].Value = JsonSerializer.Serialize(userRole);

                        return RedirectToAction("Index", "Home", null);
                    }
                }
            }
            return View("Error", new ErrorViewModel { Message = "Invalid login or password", ViewName = "Index", ControllerName = "Login" });
        }
        [HttpPost]
        public ActionResult SignIn(UserViewModel user)
        {
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