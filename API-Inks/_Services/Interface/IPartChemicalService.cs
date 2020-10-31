using INK_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Services.Interface
{
    public interface IPartChemicalService : IECService<PartInkChemicalDtos>
    {
        // Task<bool> Add (InkDto entity);
        // Task<bool> AddRangeAsync(List<InkForImportExcelDto> model);
        // Task<object> GetGluesByScheduleID(int id);
        // Task<bool> Deletes(int id);
        // Task<object> SaveGlue(PartInkChemicalDto obj);


        // Task ImportExcel(List<InkForImportExcelDto> inkDto);
    }
}
