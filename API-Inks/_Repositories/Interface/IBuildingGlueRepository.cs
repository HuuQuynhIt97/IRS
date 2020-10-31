using INK_API.Data;
using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Repositories.Interface
{
   public interface IBuildingGlueRepository: IECRepository<BuildingGlue>
    {
        Task<object> GetBuildingGlueByModelNameID(int modelNameID);
    }
}
