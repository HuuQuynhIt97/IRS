using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INK_API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using INK_API._Repositories.Interface;
using INK_API._Services.Interface;
using INK_API.DTO;
using INK_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace INK_API._Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repoComment;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public CommentService(ICommentRepository repoComment, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoComment = repoComment;

        }

        public async Task<bool> Add(CommentDto model)
        {
            var comment = _mapper.Map<Comment>(model);
            _repoComment.Add(comment);
            return await _repoComment.SaveAll();
        }

        public async Task<bool> Delete(object id)
        {
            var Comment = _repoComment.FindById(id);
            _repoComment.Remove(Comment);
            return await _repoComment.SaveAll();
        }

        public Task<List<CommentDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommentDto>> GetAllCommentByScheduleID(int scheduleID)
        {
           return await _repoComment.FindAll().Where(x => x.ScheduleID == scheduleID).ProjectTo<CommentDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        public CommentDto GetById(object id)
        {
            return _mapper.Map<Comment, CommentDto>(_repoComment.FindById(id));
        }

        public Task<PagedList<CommentDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<CommentDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(CommentDto model)
        {
            var comment = _mapper.Map<Comment>(model);
            _repoComment.Update(comment);
            return await _repoComment.SaveAll();
        }
    }
}