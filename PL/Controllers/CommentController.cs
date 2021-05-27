using AutoMapper;
using BLL.DTO;
using BLL.Managers;
using PL.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class CommentController : Controller
    {
        private readonly IManager<CommentDto> _commentManager;
        private readonly IClientManager _clientManager;
        private readonly IMapper _mapper;
        public CommentController(IManager<CommentDto> manager, IClientManager clientManager, IMapper mapper)
        {
            _commentManager = manager;
            _clientManager = clientManager;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var items = _mapper.Map<ICollection<CommentViewModel>>(_commentManager.GetAll());
            return View(items);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
                return View();

            comment.ClientId = _clientManager.GetByUserId(JsonSerializer.Deserialize<UserRoleViewModel>
                (HttpContext.Request.Cookies["user"].Value).User.Id).Id;
            _commentManager.Create(_mapper.Map<CommentDto>(comment));
            return RedirectToAction("Index", "Comment", null);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            CommentViewModel pizza = _mapper.Map<CommentViewModel>(_commentManager.GetById((int)id));
            return View(pizza);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _commentManager.Delete(id);
            return RedirectToAction("Index", "Comment", null);
        }
    }
}