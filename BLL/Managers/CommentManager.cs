using AutoMapper;
using BLL.DTO;
using DataAccessLevel.Entities;
using DataAccessLevel.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Managers
{
    internal class CommentManager : IManager<CommentDto>
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;
        public CommentManager(IRepository<Comment> repository, IMapper mapper)
        {
            _commentRepository = repository;
            _mapper = mapper;
        }
        public ICollection<CommentDto> GetAll()
        {
            return _mapper.Map<IEnumerable<CommentDto>>(_commentRepository.GetAll()).ToList();
        }
        public CommentDto GetById(int id)
        {
            return _mapper.Map<CommentDto>(_commentRepository.GetById(id));
        }
        public int Create(CommentDto comment)
        {
            return _commentRepository.Create(_mapper.Map<Comment>(comment));
        }
        public void Update(CommentDto comment)
        {
            _commentRepository.Update(_mapper.Map<Comment>(comment));
        }
        public void Delete(int id)
        {
            _commentRepository.Delete(id);
        }
    }
}
