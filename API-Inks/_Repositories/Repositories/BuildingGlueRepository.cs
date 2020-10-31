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
    public class BuildingGlueRepository : ECRepository<BuildingGlue>, IBuildingGlueRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BuildingGlueRepository(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<object> GetBuildingGlueByModelNameID(int modelNameID)
        {
            throw new System.NotImplementedException();
        }
    }
}