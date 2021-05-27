using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    class OrderManager : IManager<OrderDto>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Accounting> _accountingRepository;
        private readonly IRepository<Pizza> _pizzaRepository;

        private readonly IMapper _mapper;
        public OrderManager(IRepository<Order> orderRepository, IRepository<Client> clientRepository, IRepository<Employee> employeeRepository,
            IRepository<Accounting> accountingRepository, IRepository<Pizza> pizzaRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _accountingRepository = accountingRepository;
            _pizzaRepository = pizzaRepository;
            _mapper = mapper;
        }
        public ICollection<OrderDto> GetAll()
        {
            ICollection<OrderItemDto> orders = _mapper.Map<IEnumerable<OrderItemDto>>(_orderRepository.GetAll()).ToList();
            ICollection<ClientDto> clients = _mapper.Map<IEnumerable<ClientDto>>(_clientRepository.GetAll()).ToList();
            ICollection<EmployeeDto> employees = _mapper.Map<IEnumerable<EmployeeDto>>(_employeeRepository.GetAll()).ToList();
            ICollection<AccountingDto> accounting = _mapper.Map<IEnumerable<AccountingDto>>(_accountingRepository.GetAll()).ToList();
            ICollection<PizzaDto> pizzas = _mapper.Map<IEnumerable<PizzaDto>>(_pizzaRepository.GetAll()).ToList();

            var newPizzas = (from p in pizzas
                             join a in accounting on p.Id equals a.PizzaId
                             select new
                             {
                                 pizza = new PizzaAccountingDto { PizzaObject = p, Quantity = a.Quantity },
                                 a.OrderId
                             }).ToList();

            var groupPizzas = from p in newPizzas
                              group p by p.OrderId into g
                              select new { g.Key, pizzas1 = from p in g select p.pizza };

            var newOrders = (from o in orders
                             join c in clients on o.ClientId equals c.Id into nj
                             from c in nj.DefaultIfEmpty()
                             join e in employees on o.EmployeeId equals e.Id into ps
                             from e in ps.DefaultIfEmpty()
                             join p in groupPizzas on o.Id equals p.Key
                             select new OrderDto
                             {
                                 Id = o.Id,
                                 Date = o.Date,
                                 Client = c,
                                 Employee = e,
                                 Pizzas = p.pizzas1,
                                 AccountingImplementation = o.AccountingImplementation
                             }).ToList();

            return newOrders;
        }
        public OrderDto GetById(int id)
        {
            OrderItemDto order = _mapper.Map<OrderItemDto>(_orderRepository.GetById(id));
            ICollection<ClientDto> clients = _mapper.Map<IEnumerable<ClientDto>>(_clientRepository.GetAll()).ToList();
            ICollection<EmployeeDto> employees = _mapper.Map<IEnumerable<EmployeeDto>>(_employeeRepository.GetAll()).ToList();
            ICollection<AccountingDto> accounting = _mapper.Map<IEnumerable<AccountingDto>>(_accountingRepository.GetAll()).ToList();
            ICollection<PizzaDto> pizzas = _mapper.Map<IEnumerable<PizzaDto>>(_pizzaRepository.GetAll()).ToList();

            var newPizzas = (from p in pizzas
                             join a in accounting on p.Id equals a.PizzaId
                             select new
                             {
                                 pizza = new PizzaAccountingDto { PizzaObject = p, Quantity = a.Quantity },
                                 a.OrderId
                             }).ToList();

            var groupPizzas = from p in newPizzas
                              group p by p.OrderId into g
                              select new { g.Key, pizzas1 = from p in g select p.pizza };

            var newOrders = (from p in groupPizzas
                             where order.Id == p.Key
                             join e in employees on order.EmployeeId equals e.Id into ps
                             from e in ps.DefaultIfEmpty()
                             join c in clients on order.ClientId equals c.Id into nj
                             from c in nj.DefaultIfEmpty()
                             select new OrderDto
                             {
                                 Id = order.Id,
                                 Date = order.Date,
                                 Client = c,
                                 Employee = e,
                                 Pizzas = p.pizzas1,
                                 AccountingImplementation = order.AccountingImplementation
                             }).ToList();

            return newOrders.First();
        }
        public int Create(OrderDto order)
        {
            var orderItem = new OrderItemDto { Date = order.Date, ClientId = order.Client.Id, AccountingImplementation = order.AccountingImplementation };
            orderItem.Id = _orderRepository.Create(_mapper.Map<Order>(orderItem));
            AccountingDto accounting = new AccountingDto();
            foreach (var item in order.Pizzas)
            {
                accounting.OrderId = orderItem.Id;
                accounting.PizzaId = item.PizzaObject.Id;
                accounting.Quantity = item.Quantity;
                _accountingRepository.Create(_mapper.Map<Accounting>(accounting));
            }
            return orderItem.Id;
        }
        public void Update(OrderDto order)
        {
            OrderItemDto orderItemDto = new OrderItemDto
            {
                Id = order.Id,
                ClientId = order.Client.Id,
                Date = order.Date,
                EmployeeId = order.Employee.Id,
                AccountingImplementation = order.AccountingImplementation
            };
            _orderRepository.Update(_mapper.Map<Order>(orderItemDto));
        }
        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }
    }
}
