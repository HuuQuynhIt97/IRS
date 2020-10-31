using INK_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Services.Interface
{
    public interface IRoleService : IECService<RoleDto>
    {
        // Task<bool> Add (InkDto entity);
        // Task<bool> AddRangeAsync(List<InkForImportExcelDto> model);
        // Task ImportExcel(List<InkForImportExcelDto> inkDto);
        Task<List<RoleDto>> GetAllRole();
        Task<List<RoleUserDto>> GetAllRoleUser();
         Task<object> MapRoleUser(int userid, int roleID);
         Task<object> GetRoleByUserID(int userid);
         Task<bool> CheckLoginByUser(int userid);
         Task<object> LockUser(int userid);
    }
}
