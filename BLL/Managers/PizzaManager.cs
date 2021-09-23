using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    internal class PizzaManager : IManager<PizzaDto>
    {
        private readonly IRepository<Pizza> _pizzaRepository;
        private readonly IMapper _mapper;
        public PizzaManager(IRepository<Pizza> repository, IMapper mapper)
        {
            _pizzaRepository = repository;
            _mapper = mapper;
        }
        public ICollection<PizzaDto> GetAll()
        {
            return _mapper.Map<IEnumerable<PizzaDto>>(_pizzaRepository.GetAll()).Where(p => p.OnSale).ToList();
        }
        public PizzaDto GetById(int id)
        {
            return _mapper.Map<PizzaDto>(_pizzaRepository.GetById(id));
        }
        public int Create(PizzaDto pizza)
        {
            pizza.OnSale = true;
            return _pizzaRepository.Create(_mapper.Map<Pizza>(pizza));
        }
        public void Update(PizzaDto pizza)
        {
            _pizzaRepository.Update(_mapper.Map<Pizza>(pizza));
        }
        public void Delete(int id)
        {
            var pizza = _mapper.Map<PizzaDto>(_pizzaRepository.GetById(id));

            pizza.OnSale = false;

            _pizzaRepository.Update(_mapper.Map<Pizza>(pizza));
        }
    }
}
