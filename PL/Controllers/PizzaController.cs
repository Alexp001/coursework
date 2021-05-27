using AutoMapper;
using BLL.DTO;
using BLL.Managers;
using System.Collections.Generic;
using System.Web.Mvc;
using PL.Models;

namespace PL.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IManager<PizzaDto> _pizzaManager;
        private readonly IMapper _mapper;
        public PizzaController(IManager<PizzaDto> manager, IMapper mapper)
        {
            _pizzaManager = manager;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var items = _mapper.Map<ICollection<PizzaViewModel>>(_pizzaManager.GetAll());
            return View(items);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PizzaViewModel pizza)
        {
            if (!ModelState.IsValid)
                return View();
            _pizzaManager.Create(_mapper.Map<PizzaDto>(pizza));
            return RedirectToAction("Index", "Pizza", null);
        }
        [HttpGet]
        public ActionResult Update(int? id)
        {
            PizzaViewModel pizza = _mapper.Map<PizzaViewModel>(_pizzaManager.GetById((int)id));
            return View(pizza);
        }
        [HttpPost]
        public ActionResult Update(PizzaViewModel pizza)
        {
            if (!ModelState.IsValid)
                return View();
            _pizzaManager.Update(_mapper.Map<PizzaDto>(pizza));
            return RedirectToAction("Index", "Pizza", null);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            PizzaViewModel pizza = _mapper.Map<PizzaViewModel>(_pizzaManager.GetById((int)id));
            return View(pizza);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _pizzaManager.Delete(id);
            }
            catch
            {
                return View("Error", new ErrorViewModel { Message = "This pizza has already been ordered. If deleted, the order information will be lost.",
                    ViewName = "Index", ControllerName = "Pizza" });
            }
            return RedirectToAction("Index", "Pizza", null);
        }
    }
}