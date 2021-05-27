using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.Json;
using PL.Models;
using BLL.Managers;
using AutoMapper;
using BLL.DTO;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        public AccountController(IUserManager manager, IMapper mapper)
        {
            _userManager = manager;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            return View(JsonSerializer.Deserialize<UserRoleViewModel>(HttpContext.Request.Cookies["user"].Value).User);
        }
        public ActionResult LogOut()
        {
            HttpContext.Response.Cookies["user"].Value = "";
            return RedirectToAction("Index", "Home", null);
        }
        [HttpGet]
        public ActionResult ChangePassword(string login)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (!ModelState.IsValid)
                return View();
            UserViewModel user = JsonSerializer.Deserialize<UserRoleViewModel>(HttpContext.Request.Cookies["user"].Value).User;
            if (user.Password == change.OldPassword)
            {
                user.Password = change.NewPassword;
                _userManager.Update(_mapper.Map<UserDto>(user));
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                return View("Error", new ErrorViewModel { Message = "Invalid old password", ViewName = "Index", ControllerName = "Account" });
            }
        }
    }
}