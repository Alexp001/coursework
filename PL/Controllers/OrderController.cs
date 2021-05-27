using AutoMapper;
using BLL.DTO;
using BLL.Managers;
using PL.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Text.Json;
using System;
using System.Linq;

namespace PL.Controllers
{
    public class OrderController : Controller
    {
        private List<PizzaAccountingViewModel> _pizzas = new List<PizzaAccountingViewModel>();
        private readonly IClientManager _clientIdManager;
        private readonly IEmployeeManager _employeeIdManager;
        private readonly IManager<OrderDto> _orderManager;
        private readonly IManager<PizzaDto> _pizzaManager;
        private readonly IMapper _mapper;
        public OrderController(IClientManager clientIdManager, IEmployeeManager employeeManager, IManager<OrderDto> manager, IManager<PizzaDto> manager1, IMapper mapper)
        {
            _clientIdManager = clientIdManager;
            _employeeIdManager = employeeManager;
            _orderManager = manager;
            _pizzaManager = manager1;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var items = _mapper.Map<ICollection<OrderViewModel>>(_orderManager.GetAll());
            return View(items);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (HttpContext.Request.Cookies["listPizzas"] == null)
                HttpContext.Response.Cookies["listPizzas"].Value = JsonSerializer.Serialize(new List<PizzaAccountingViewModel>());

            var items = _mapper.Map<ICollection<PizzaViewModel>>(_pizzaManager.GetAll());
            return View(items);
        }
        [HttpPost]
        public ActionResult Confirm()
        {
            _pizzas = JsonSerializer.Deserialize<List<PizzaAccountingViewModel>>(HttpContext.Request.Cookies["listPizzas"].Value);
            if (_pizzas == null || _pizzas.Count == 0)
                return View("Error", new ErrorViewModel { Message = "Basket is empty", ViewName = "ShoppingBasket", ControllerName = "Order" });
            var order = new OrderViewModel
            {
                Pizzas = _pizzas,
                Date = DateTime.Now
            };
            UserRoleViewModel client = JsonSerializer.Deserialize<UserRoleViewModel>(HttpContext.Request.Cookies["user"].Value);
            order.Client = _mapper.Map<ClientViewModel>(_clientIdManager.GetByUserId(client.User.Id));
            _orderManager.Create(_mapper.Map<OrderDto>(order));
            HttpContext.Response.Cookies["listPizzas"].Value = JsonSerializer.Serialize(new List<PizzaAccountingViewModel>());
            return RedirectToAction("Index", "Home", null);
        }
        [HttpGet]
        public ActionResult AddPizza(int id, string name, double price, string image)
        {
            return View(new PizzaAccountingViewModel { PizzaObject = new PizzaViewModel { Id = id, Name = name, Price = price, Image = image } } );
        }
        [HttpPost]
        public ActionResult AddPizza(PizzaAccountingViewModel pizza)
        {
            if (!ModelState.IsValid)
                return View(pizza);
            _pizzas = JsonSerializer.Deserialize<List<PizzaAccountingViewModel>>(HttpContext.Request.Cookies["listPizzas"].Value);
            for (int i = 0; i < _pizzas.Count; i++)
            {
                if (_pizzas[i].PizzaObject.Id == pizza.PizzaObject.Id)
                {
                    _pizzas[i].Quantity += pizza.Quantity;
                    HttpContext.Response.Cookies["listPizzas"].Value = JsonSerializer.Serialize(_pizzas);
                    return RedirectToAction("Create", "Order", null);
                }
            }
            _pizzas.Add(pizza);
            HttpContext.Response.Cookies["listPizzas"].Value = JsonSerializer.Serialize(_pizzas);
            return RedirectToAction("Create", "Order", null);
        }
        public ActionResult ShoppingBasket()
        {
            _pizzas = JsonSerializer.Deserialize<List<PizzaAccountingViewModel>>(HttpContext.Request.Cookies["listPizzas"].Value);
            return View(_pizzas);
        }
        public ActionResult ShowClient(string name, string address, string email, string phone)
        {
            return View(new ClientViewModel { Name = name, Address = address, Email = email, Phone = phone });
        }
        public ActionResult ShowEmployee(string name, string address, string email, string phone, double salary, string position)
        {
            return View(new EmployeeViewModel { Name = name, Address = address, Email = email, Phone = phone, Salary = salary, Position = position });
        }
        public ActionResult ShowOrder()
        {
            var items = _mapper.Map<ICollection<OrderViewModel>>(_orderManager.GetAll());
            return View(items);
        }
        public ActionResult TakeOrder(int id)
        {
            var item = _mapper.Map<OrderViewModel>(_orderManager.GetById(id));
            EmployeeViewModel employee = _mapper.Map<EmployeeViewModel>(_employeeIdManager.GetByUserId(JsonSerializer.Deserialize<UserRoleViewModel>(HttpContext.Request.Cookies["user"].Value).User.Id));
            item.Employee = employee;
            _orderManager.Update(_mapper.Map<OrderDto>(item));
            return RedirectToAction("ShowOrder", "Order", null);
        }
        public ActionResult ShowOrderForExecutor()
        {
            var items = _mapper.Map<ICollection<OrderViewModel>>(_orderManager.GetAll());
            return View(items.Where(i => i.Employee != null));
        }
        public ActionResult ExecuteOrder(int id, bool flag)
        {
            var item = _mapper.Map<OrderViewModel>(_orderManager.GetById(id));
            item.AccountingImplementation = flag;
            _orderManager.Update(_mapper.Map<OrderDto>(item));
            return RedirectToAction("ShowOrderForExecutor", "Order", null);
        }
        public ActionResult DeletePizza(int id)
        {
            _pizzas = JsonSerializer.Deserialize<List<PizzaAccountingViewModel>>(HttpContext.Request.Cookies["listPizzas"].Value);
            _pizzas.RemoveAll(i => i.PizzaObject.Id == id);
            HttpContext.Response.Cookies["listPizzas"].Value = JsonSerializer.Serialize(_pizzas);
            return RedirectToAction("ShoppingBasket", "Order", null);

        }
    }
}