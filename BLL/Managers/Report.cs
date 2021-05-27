using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    public class Report : IReport
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Accounting> _accountingRepository;
        private readonly IRepository<Pizza> _pizzaRepository;
        private readonly IMapper _mapper;
        public Report(IRepository<Order> orderRepository, IRepository<Client> clientRepository, IRepository<Employee> employeeRepository,
            IRepository<Accounting> accountingRepository, IRepository<Pizza> pizzaRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _accountingRepository = accountingRepository;
            _pizzaRepository = pizzaRepository;
            _mapper = mapper;
        }
        public IEnumerable<OrderDto> GetOrdersByDate(DateTime date1, DateTime date2)
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
                             where o.Date > date1 && o.Date < date2
                             select new OrderDto { Id = o.Id, Date = o.Date, Client = c, Employee = e, Pizzas = p.pizzas1 }).ToList();

            return newOrders;
        }
        public IEnumerable<ReportByPizzaCountDto> GetReportByPizzaCount(DateTime date1, DateTime date2)
        {
            var orders = GetOrdersByDate(date1, date2);

            var report = from o in orders
                             from p in o.Pizzas
                             group p by p.PizzaObject into g
                             select new ReportByPizzaCountDto { PizzaName = g.Key.Name, Count = g.Sum(i => i.Quantity)};
            
            return report;
        }
        public IEnumerable<ReportByPizzaPriceDto> GetReportByPizzaPrice(DateTime date1, DateTime date2)
        {
            var orders = GetOrdersByDate(date1, date2);

            var report = (from o in orders
                          from p in o.Pizzas
                          group p by p.PizzaObject into g
                          select new ReportByPizzaPriceDto { PizzaName = g.Key.Name,  TotalPrice = g.Sum(i => i.Quantity * i.PizzaObject.Price)}).ToList();

            return report;
        }
        public IEnumerable<ReportByEmployeeCountDto> GetReportByEmployeeCount(DateTime date1, DateTime date2)
        {
            var orders = GetOrdersByDate(date1, date2);

            var report = from o in orders
                         group o by o.Employee into g
                         select new ReportByEmployeeCountDto { EmployeeName = g.Key?.Name ?? "Order not accepted", Count = g.Count() };

            return report;
        }
        public IEnumerable<ReportByEmployeePriceDto> GetReportByEmployeePrice(DateTime date1, DateTime date2)
        {
            var orders = GetOrdersByDate(date1, date2);

            var report = (from o in orders
                          group o by o.Employee into g
                          select new ReportByEmployeePriceDto { EmployeeName = g.Key?.Name ?? "Order not accepted", TotalPrice = g.Sum(i => i.Pizzas.Sum(item => item.Quantity * item.PizzaObject.Price )) }).ToList();

            return report;
        }

        public double GetTotalPrice(DateTime date1, DateTime date2)
        {
            var orders = GetOrdersByDate(date1, date2);
            double cost = orders.Sum(i => i.Pizzas.Sum(j => j.Quantity * j.PizzaObject.Price));
            return cost;
        }
        public IEnumerable<ReportByClientDto> GetReportByClient(DateTime date1, DateTime date2)
        {
            var orders = GetOrdersByDate(date1, date2);

            var report = from o in orders
                         group o by o.Client into g
                         select new ReportByClientDto { ClientName = g.Key?.Name ?? "Client not found", Count = g.Count() };

            return report;
        }

    }
}
