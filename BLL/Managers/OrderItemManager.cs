using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    internal class OrderItemManager : IManager<OrderItemDto>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        public OrderItemManager(IRepository<Order> repository, IMapper mapper)
        {
            _orderRepository = repository;
            _mapper = mapper;
        }
        public ICollection<OrderItemDto> GetAll()
        {
            return _mapper.Map<IEnumerable<OrderItemDto>>(_orderRepository.GetAll()).ToList();
        }
        public OrderItemDto GetById(int id)
        {
            return _mapper.Map<OrderItemDto>(_orderRepository.GetById(id));
        }
        public int Create(OrderItemDto order)
        {
            return _orderRepository.Create(_mapper.Map<Order>(order));
        }
        public void Update(OrderItemDto order)
        {
            _orderRepository.Update(_mapper.Map<Order>(order));
        }
        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }
    }
}
