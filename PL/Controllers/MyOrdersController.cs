using AutoMapper;
using BLL.DTO;
using BLL.Managers;
using PL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MyOrdersController : Controller
    {
        private readonly IManager<OrderDto> _orderManager;
        private readonly IClientManager _clientIdManager;
        private readonly IMapper _mapper;

        public MyOrdersController(IManager<OrderDto> manager, IClientManager manager1, IMapper mapper)
        {
            _orderManager = manager;
            _clientIdManager = manager1;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var items = _mapper.Map<ICollection<OrderViewModel>>(_orderManager.GetAll());
            var user = JsonSerializer.Deserialize<UserRoleViewModel>(HttpContext.Request.Cookies["user"].Value);
            var client = _mapper.Map<ClientViewModel>(_clientIdManager.GetByUserId(user.User.Id));

            return View(items.Where(i => i.Client?.Id == client.Id));
        }
    }
}