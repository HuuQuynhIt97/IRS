using INK_API.DTO;
using INK_API.Helpers;
using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Services.Interface
{
   public interface IBuildingUserService : IECService<BuildingUserDto>
    {
        Task<object> MappingUserWithBuilding(BuildingUserDto buildingUserDto);
        Task<object> RemoveBuildingUser(BuildingUserDto buildingUserDto);
        Task<List<BuildingUserDto>> GetBuildingUserByBuildingID(int buildingID);
        Task<object> GetBuildingByUserID(int userid);
        Task<object> MapBuildingUser(int userid, int buildingid);
    }
}
