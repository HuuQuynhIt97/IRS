using INK_API.DTO;
using INK_API.Helpers;
using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Services.Interface
{
   public interface IBuildingService : IECService<BuildingDto>
    {
        Task<IEnumerable<HierarchyNode<BuildingDto>>> GetAllAsTreeView();
        Task<List<BuildingDto>> GetBuildings();
        Task<object> CreateMainBuilding(BuildingDto buildingDto);
        Task<object> CreateSubBuilding(BuildingDto buildingDto);
    }
}
