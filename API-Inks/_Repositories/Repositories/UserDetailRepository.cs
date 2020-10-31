using INK_API._Repositories.Interface;
using INK_API.Data;
using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Repositories.Repositories
{
    public class UserDetailRepository : ECRepository<UserDetail>, IUserDetailRepository
    {
        private readonly DataContext _context;
        public UserDetailRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
