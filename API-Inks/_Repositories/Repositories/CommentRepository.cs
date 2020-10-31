using System.Threading.Tasks;
using INK_API._Repositories.Interface;
using INK_API.Data;
using INK_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using INK_API.DTO;
using System.Collections.Generic;
using AutoMapper;

namespace INK_API._Repositories.Repositories
{
    public class CommentRepository : ECRepository<Comment>, ICommentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommentRepository(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

   
    }
}