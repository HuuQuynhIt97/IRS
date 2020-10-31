using INK_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Services.Interface
{
    public interface IChemicalService : IECService<ChemicalDto>
    {
        Task<bool> Add (ChemicalDto entity);
        Task<bool> UpdateAsync (ChemicalUpdateDto entity);
        Task<object> GetChemicalBySupplier (int id);
        Task<bool> AddRangeAsync(List<ChemicalForImportExcelDto> model);
        Task ImportExcel(List<ChemicalForImportExcelDto> chemicalDto);
    }
}
