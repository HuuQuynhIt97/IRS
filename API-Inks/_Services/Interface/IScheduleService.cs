using INK_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API._Services.Interface
{
    public interface IScheduleService : IECService<ScheduleDto>
    {
        Task ImportExcel(List<ScheduleDtoForImportExcel> scheduleDto);
        Task<List<ScheduleUpdateDto>> GetDetailSchedule(int scheduleID);
        Task<bool> EditSchedule(ScheduleUpdateEditDto obj);
        Task<bool> EditPartSchedule(ScheduleUpdatePartDTO obj);
        Task<bool> CreateSchedule(ScheduleDtoForImportExcel obj);
        Task<object> GetAllAsync();
        Task<object> Done(int bpfcID);
        Task<object> UpdateProductionDate(string value , int scheduleID);
        Task<object> Approve(int scheduleID ,int userid);
        Task<object> Reject(int scheduleID, int userid);
         Task<object> Release(int scheduleID, int userid);
    }
}
