using AutoMapper;
using INK_API._Repositories.Interface;
using INK_API.Data;
using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Repositories.Repositories
{
    public class BuildingRepository : ECRepository<Building>, IBuildingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BuildingRepository(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
    
    }
}
