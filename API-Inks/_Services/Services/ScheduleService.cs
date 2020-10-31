using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INK_API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using INK_API._Repositories.Interface;
using INK_API._Services.Interface;
using INK_API.DTO;
using INK_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace INK_API._Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IProcessRepository _repoProcess;
        private readonly IChemicalRepository _repoChemical;
        private readonly IScheduleRepository _repoSchedule;
        private readonly IScheduleUpdateRepository _repoScheduleUpdate;
        private readonly ISupplierRepository _repoSup;
        private readonly IInkRepository _repoInk;
        private readonly IInkObjectRepository _repoObject;
        private readonly IPartRepository _repoPart;
        private readonly ICommentRepository _repoComment;
        private readonly ISchedulePartRepository _repoSchedulePart;
        private readonly ITreatmentWayRepository _repoTreatmentWay;

        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ScheduleService(
            ITreatmentWayRepository repoTreatmentWay,
            ICommentRepository repoComment,
            IScheduleUpdateRepository repoScheduleUpdate,
            ISchedulePartRepository repoSchedulePart,
            IPartRepository repoPart,
            IInkObjectRepository repoObject,
            IScheduleRepository repoSchedule,
            IChemicalRepository repoChemical,
            IProcessRepository repoProcess,
            ISupplierRepository repoSup,
            IInkRepository repoInk,
            IMapper mapper,
            MapperConfiguration configMapper)
        {
            _repoTreatmentWay = repoTreatmentWay;
            _repoScheduleUpdate = repoScheduleUpdate;
            _configMapper = configMapper;
            _mapper = mapper;
            _repoSchedulePart = repoSchedulePart;
            _repoInk = repoInk;
            _repoSup = repoSup;
            _repoProcess = repoProcess;
            _repoChemical = repoChemical;
            _repoSchedule = repoSchedule;
            _repoObject = repoObject;
            _repoPart = repoPart;
            _repoComment = repoComment;

        }

        public async Task<object> UpdateProductionDate(string value, int scheduleID)
        {
            var dt = Convert.ToDateTime(value);
            var item = _repoScheduleUpdate.FindById(scheduleID);
            var process = await _repoProcess.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(item.Treatment.ToUpper()));
            var ProcessID = 0;
            if (process != null)
            {
                ProcessID = process.ID;
            }
            try
            {
                item.ProductionDate = dt.Date;
                if (ProcessID == 1)
                {
                    item.EstablishDate = dt.Date.AddDays(-30);
                }
                else
                {
                    item.EstablishDate = dt.Date.AddDays(-15);
                }
                return await _repoScheduleUpdate.SaveAll();
            }
            catch (Exception)
            {
                return false;
            }
            throw new System.NotImplementedException();
        }
        public async Task<object> Release(int scheduleID, int userid)
        {
            var item = _repoScheduleUpdate.FindById(scheduleID);
            try
            {
                item.FinishedStatus = true;
                item.ApprovalStatus = true;
                item.ApprovalBy = userid;
                return await _repoScheduleUpdate.SaveAll();
            }
            catch (Exception)
            {
                return false;
            }
            throw new System.NotImplementedException();
        }

        public async Task<bool> CreateSchedule(ScheduleDtoForImportExcel obj)
        {
            try
            {
                var process = await _repoProcess.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(obj.Process.ToUpper()));
                var ProcessID = 0;
                if (process != null)
                {
                    ProcessID = process.ID;
                }
                var check = await _repoScheduleUpdate.FindAll().FirstOrDefaultAsync(y => y.ModelName.ToUpper().Equals(obj.ModelName.ToUpper()) && y.ModelNo.ToUpper().Equals(obj.ModelNo.ToUpper()) && y.ArticleNo.ToUpper().Equals(obj.ArticleNo.ToUpper()) && y.Treatment.ToUpper().Equals(obj.Process.ToUpper()) && y.Process.ToUpper().Equals(obj.Object.ToUpper()));
                if (check != null)
                {
                    var listCheck = await _repoPart.FindAll().Where(x => x.ScheduleID == check.ID).ToListAsync();
                    if (obj.listPart.Count == 0)
                    {
                        var checks = listCheck.Select(x => x.Name).Contains("Whole");
                        if (!listCheck.Select(x => x.Name).Contains("Whole"))
                        {

                            _repoPart.Add(new Part
                            {
                                Name = "Whole",
                                ScheduleID = check.ID,
                                Status = true
                            });
                            await _repoPart.SaveAll();
                        }
                        // var dataPart = new Part { Name = "Whole", ScheduleID = check.ID, Status = true };
                        // _repoPart.Add(dataPart);
                        // await _repoPart.SaveAll();
                    }
                    else
                    {
                        foreach (var part in obj.listPart)
                        {

                            if (!listCheck.Select(x => x.Name).Contains(part.value))
                            {

                                _repoPart.Add(new Part
                                {
                                    ScheduleID = check.ID,
                                    Name = part.value,

                                });
                                await _repoPart.SaveAll();
                            }
                        }

                    }

                }
                if (check == null)
                {
                    // add schedule
                    var schedules = new SchedulesUpdate();

                    schedules.ModelName = obj.ModelName;
                    schedules.ModelNo = obj.ModelNo;
                    schedules.ArticleNo = obj.ArticleNo;
                    schedules.Treatment = obj.Process;
                    schedules.Process = obj.Object;

                    // schedules.a = x.Key.ArtProcessID;
                    schedules.CreatedBy = obj.CreatedBy;
                    schedules.ApprovalBy = 0;
                    if (ProcessID == 1)
                    {
                        schedules.ProductionDate = obj.ProductionDate;
                        schedules.EstablishDate = obj.ProductionDate.AddDays(-30);
                    }
                    else
                    {
                        schedules.ProductionDate = obj.ProductionDate;
                        schedules.EstablishDate = obj.ProductionDate.AddDays(-15);
                    }
                    schedules.CreatedDate = DateTime.Now;
                    schedules.UpdateTime = DateTime.Now;
                    schedules.ApprovalStatus = false;
                    schedules.FinishedStatus = false;
                    _repoScheduleUpdate.Add(schedules);
                    await _repoScheduleUpdate.SaveAll();

                    // luu db xong moi add part

                    if (obj.listPart.Count == 0)
                    {
                        var dataPart = new Part { Name = "Whole", ScheduleID = schedules.ID, Status = true };
                        _repoPart.Add(dataPart);
                        await _repoPart.SaveAll();
                    }
                    else
                    {
                        foreach (var part in obj.listPart)
                        {
                            _repoPart.Add(new Part
                            {
                                ScheduleID = schedules.ID,
                                Name = part.value,

                            });
                            await _repoPart.SaveAll();
                        }
                    }
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }

        }
        public async Task<object> Reject(int scheduleID, int userid)
        {
            var item = _repoScheduleUpdate.FindById(scheduleID);
            try
            {
                item.FinishedStatus = false;
                item.ApprovalStatus = false;
                item.ApprovalBy = userid;
                var remark = _repoComment.FindAll().Where(x => x.ScheduleID == scheduleID).OrderBy(y => y.CreatedDate).LastOrDefault();
                if (remark == null)
                {
                    //rollback
                    return new
                    {
                        status = false,
                        message = "Please Enter new comment to Reject Schedule !"
                    };
                }
                var timePresent = DateTime.Now;
                var timeLast = Convert.ToDateTime(remark.CreatedDate);
                var compare = DateTime.Compare(timePresent.Date, timeLast.Date);
                if (compare > 0)
                {
                    return new
                    {
                        status = false,
                        message = "The Lastest comment allow 1 Hour , Please Enter new comment to Reject Schedule !",
                        userId = item.CreatedBy
                    };
                }
                if (compare == 0 && timePresent.TimeOfDay.TotalHours - timeLast.TimeOfDay.TotalHours > 1)
                {
                    return new
                    {
                        status = false,
                        message = "The Lastest comment allow 1 Hour , Please Enter new comment to Reject Schedule !",
                        userId = item.CreatedBy
                    };
                }
                var status = await _repoScheduleUpdate.SaveAll();
                return new
                {
                    status = status,
                    message = "The schedule has been rejected!!!",
                    userId = item.CreatedBy
                };

            }
            catch (Exception ex)
            {
                return new
                {
                    status = false,
                    message = ex.Message
                };
            }
        }

        private async Task<ScheduleUpdateDto> AddSchedule(ScheduleDtoForImportExcel scheduleDto)
        {
            var result = new ScheduleUpdateDto();
            var process = await _repoProcess.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(scheduleDto.Process.ToUpper()));
            if (process != null)
            {
                result.ProcessID = process.ID;
            }
            result.ModelName = scheduleDto.ModelName;
            result.ModelNo = scheduleDto.ModelNo;
            result.ArticleNo = scheduleDto.ArticleNo;
            result.Treatment = scheduleDto.Process;

            result.Process = scheduleDto.Object;
            result.Part = scheduleDto.Part;

            result.CreatedBy = scheduleDto.CreatedBy;
            result.ProductionDate = scheduleDto.ProductionDate;

            return result;

        }
        public async Task ImportExcel2(List<ScheduleDtoForImportExcel> scheduleDto)
        {
            // try
            // {
            //     var list = new List<ScheduleDto>();
            //     var listExist = new List<ScheduleDto>();
            //     var result = scheduleDto.DistinctBy(x => new
            //     {
            //         x.ModelName,
            //         x.ModelNo,
            //         x.ArticleNo,
            //         x.Process,
            //         x.Object,
            //         x.Part,
            //     }).Where(x => x.ModelName != "").ToList();

            //     foreach (var item in result)
            //     {
            //         var Schedule = await AddSchedule(item);
            //         list.Add(Schedule);
            //     }

            //     var listAdd = new List<Schedules>();
            //     var b = list.GroupBy(x => new
            //     {
            //         x.ModelNameID,
            //         x.ModelNoID,
            //         x.ArticleNoID,
            //         x.ArtProcessID,
            //         x.ObjectInkID,
            //         // x.Part,
            //     }).DistinctBy(x => x.Key);
            //     foreach (var x in b)
            //     {
            //         if (!await _repoSchedule.FindAll().AnyAsync(y => y.ModelNameID == x.Key.ModelNameID && y.ModelNoID == x.Key.ModelNoID && y.ArticleNoID == x.Key.ArticleNoID && y.ArtProcessID == x.Key.ArtProcessID && y.InkTblObjectID == x.Key.ObjectInkID))
            //         {
            //             // add schedule
            //             var schedules = new Schedules();

            //             schedules.ModelNameID = x.Key.ModelNameID;
            //             schedules.ModelNoID = x.Key.ModelNoID;
            //             schedules.ArticleNoID = x.Key.ArticleNoID;
            //             schedules.ArtProcessID = x.Key.ArtProcessID;
            //             schedules.InkTblObjectID = x.Key.ObjectInkID;

            //             // schedules.a = x.Key.ArtProcessID;
            //             schedules.CreatedBy = x.FirstOrDefault().CreatedBy;
            //             schedules.ApprovalBy = x.FirstOrDefault().ApprovalBy;
            //             if (x.FirstOrDefault().ProcessID == 1)
            //             {
            //                 schedules.ProductionDate = Convert.ToDateTime(x.FirstOrDefault().ProductionDate);
            //                 schedules.EstablishDate = Convert.ToDateTime(x.FirstOrDefault().ProductionDate).AddDays(-30);
            //             }
            //             else
            //             {
            //                 schedules.ProductionDate = Convert.ToDateTime(x.FirstOrDefault().ProductionDate);
            //                 schedules.EstablishDate = Convert.ToDateTime(x.FirstOrDefault().ProductionDate).AddDays(-15);
            //             }
            //             schedules.CreatedDate = DateTime.Now;
            //             schedules.UpdateTime = DateTime.Now;
            //             schedules.ApprovalStatus = x.FirstOrDefault().ApprovalStatus;
            //             schedules.FinishedStatus = x.FirstOrDefault().FinishedStatus;
            //             schedules.Season = x.FirstOrDefault().Season;
            //             schedules.ModelName = null;
            //             schedules.ModelNo = null;
            //             schedules.ArticleNo = null;
            //             _repoSchedule.Add(schedules);
            //             // listAdd.Add(schedules);
            //             await _repoSchedule.SaveAll();
            //             // add part all
            //             var dataPart = new Part { Name = "All", ObjectID = x.Key.ObjectInkID, ScheduleID = schedules.ID ,Status = true };
            //             _repoPart.Add(dataPart);
            //             await _repoPart.SaveAll();

            //              // luu db xong moi add part
            //             foreach (var part in x.ToList())
            //             {

            //                 _repoPart.Add(new Part
            //                 {
            //                     ObjectID = x.Key.ObjectInkID,
            //                     ScheduleID = schedules.ID,
            //                     Name = part.Part,

            //                 });
            //                 await _repoPart.SaveAll();
            //             }
            //         }
            //         else
            //         {
            //             // listAdd.Add(x.ToList());
            //         }

            //     }
            //     var result1 = listAdd.Where(x => x.ID > 0).ToList();
            //     var result2 = listAdd.Where(x => x.ID == 0).ToList();


            // }
            // catch (Exception ex)
            // {
            //     throw;
            // }
        }

        public async Task ImportExcel(List<ScheduleDtoForImportExcel> scheduleDto)
        {
            try
            {
                var list = new List<ScheduleUpdateDto>();
                var listExist = new List<object>();
                var result = scheduleDto.DistinctBy(x => new
                {
                    x.ModelName,
                    x.ModelNo,
                    x.ArticleNo,
                    x.Process,
                    x.Object,
                    x.Part,
                    x.ProductionDate
                }).Where(x => x.ModelName != "").ToList();

                foreach (var item in result)
                {
                    var Schedule = await AddSchedule(item);
                    list.Add(Schedule);
                }

                var listAdd = new List<SchedulesUpdate>();
                var listAdd2 = new List<ScheduleUpdateDto>();
                var b = list.GroupBy(x => new
                {
                    x.ModelName,
                    x.ModelNo,
                    x.ArticleNo,
                    x.Treatment,
                    x.Process,
                    x.ProductionDate
                }).DistinctBy(x => x.Key);
                foreach (var x in b)
                {
                    if (!await _repoScheduleUpdate.FindAll().AnyAsync(y => y.ModelName == x.Key.ModelName && y.ModelNo == x.Key.ModelNo && y.ArticleNo == x.Key.ArticleNo && y.Treatment == x.Key.Treatment && y.Process == x.Key.Process))
                    {
                        // add schedule
                        var schedules = new SchedulesUpdate();

                        schedules.ModelName = x.Key.ModelName;
                        schedules.ModelNo = x.Key.ModelNo;
                        schedules.ArticleNo = x.Key.ArticleNo;
                        schedules.Treatment = x.Key.Treatment;
                        schedules.Process = x.Key.Process;
                        // schedules.a = x.Key.ArtProcessID;
                        schedules.CreatedBy = x.FirstOrDefault().CreatedBy;
                        schedules.ApprovalBy = x.FirstOrDefault().ApprovalBy;
                        if (x.FirstOrDefault().ProcessID == 1)
                        {
                            schedules.ProductionDate = x.FirstOrDefault().ProductionDate;
                            schedules.EstablishDate = x.FirstOrDefault().ProductionDate.AddDays(-30);
                        }
                        else
                        {
                            schedules.ProductionDate = x.FirstOrDefault().ProductionDate;
                            schedules.EstablishDate = x.FirstOrDefault().ProductionDate.AddDays(-15);
                        }
                        schedules.CreatedDate = DateTime.Now;
                        schedules.UpdateTime = DateTime.Now;
                        schedules.ApprovalStatus = x.FirstOrDefault().ApprovalStatus;
                        schedules.FinishedStatus = x.FirstOrDefault().FinishedStatus;
                        _repoScheduleUpdate.Add(schedules);
                        listAdd.Add(schedules);
                        await _repoScheduleUpdate.SaveAll();

                        // luu db xong moi add part
                        foreach (var part in x.ToList())
                        {
                            if (part.Part == string.Empty)
                            {
                                var dataPart = new Part { Name = "Whole", ScheduleID = schedules.ID, Status = true };
                                _repoPart.Add(dataPart);
                                await _repoPart.SaveAll();
                            }
                            else
                            {
                                _repoPart.Add(new Part
                                {
                                    ScheduleID = schedules.ID,
                                    Name = part.Part,

                                });
                                await _repoPart.SaveAll();
                            }

                        }
                    }
                    else
                    {
                        listExist.Add(x.ToList());
                    }

                }
                var result1 = listAdd.Where(x => x.ID > 0).ToList();
                var result2 = listAdd.Where(x => x.ID == 0).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AddRangeAsync(List<ChemicalForImportExcelDto> model)
        {
            var ingredients = _mapper.Map<List<Chemical>>(model);
            ingredients.ForEach(ingredient => { ingredient.isShow = true; });
            _repoChemical.AddRange(ingredients);
            return await _repoInk.SaveAll();
        }

        public async Task<bool> Add(ChemicalDto model)
        {
            var ink = _mapper.Map<Chemical>(model);
            ink.isShow = true;
            _repoChemical.Add(ink);
            return await _repoInk.SaveAll();
        }

        public async Task<bool> Delete(object id)
        {
            var part = _repoPart.FindAll().Where(x => x.ScheduleID == id.ToInt()).ToList();
            _repoPart.RemoveMultiple(part);
            await _repoPart.SaveAll();
            var schedule = _repoScheduleUpdate.FindById(id);
            _repoScheduleUpdate.Remove(schedule);
            return await _repoScheduleUpdate.SaveAll();
        }

        public async Task<object> GetAllAsync()
        {
            var res = await _repoScheduleUpdate.FindAll().ToListAsync();
            var items = res.GroupBy(x => new
            {
                x.ModelName,
                x.ModelNo,
                x.ArticleNo,
                x.Treatment,
                x.Process,
                x.ProductionDate.Date
            }).Select(x => new ScheduleUpdateDto
            {
                ID = x.FirstOrDefault().ID,
                ModelName = x.FirstOrDefault().ModelName,
                ModelNo = x.FirstOrDefault().ModelNo,
                ArticleNo = x.FirstOrDefault().ArticleNo,
                Treatment = x.FirstOrDefault().Treatment,
                Process = x.FirstOrDefault().Process,
                FinishedStatus = x.FirstOrDefault().FinishedStatus,
                ApprovalBy = x.FirstOrDefault().ApprovalBy,
                ApprovalStatus = x.FirstOrDefault().ApprovalStatus,
                EstablishDate = x.FirstOrDefault().EstablishDate,
                ProductionDate = x.FirstOrDefault().ProductionDate,
                CreatedDate = x.FirstOrDefault().CreatedDate,
                Parts = _repoPart.FindAll().Where(y => y.ScheduleID == x.FirstOrDefault().ID).ToList().Select(x => x.Name),

            }).OrderBy(x => x.EstablishDate);
            return items;
        }

        public Task<bool> Add(ScheduleDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ScheduleDto model)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ScheduleDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ScheduleDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public ScheduleDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ScheduleUpdateDto>> GetDetailSchedule(int scheduleID)
        {
            var nameProcess = _repoScheduleUpdate.FindAll().FirstOrDefault(x => x.ID == scheduleID).Treatment;
            var idProcess = _repoProcess.FindAll().FirstOrDefault(x => x.Name == nameProcess).ID;
            var lists = await _repoScheduleUpdate.FindAll()
                .Where(x => x.ID == scheduleID)
                .ProjectTo<ScheduleUpdateDto>(_configMapper)
                .OrderByDescending(x => x.ID).Select(x => new ScheduleUpdateDto
                {
                    ID = x.ID,
                    ModelName = x.ModelName,
                    ModelNo = x.ModelNo,
                    Part = x.Part,
                    ArticleNo = x.ArticleNo,
                    Treatment = x.Treatment,
                    Process = x.Process,
                    ApprovalStatus = x.ApprovalStatus,
                    FinishedStatus = x.FinishedStatus,
                    CreatedDate = x.CreatedDate,
                    UpdateTime = x.UpdateTime,
                    CreatedBy = x.CreatedBy,
                    ProductionDate = x.ProductionDate,
                    EstablishDate = x.EstablishDate,
                    Parts = _repoPart.FindAll().Where(y => y.ScheduleID == scheduleID).ToList(),
                    Supplier = _repoSup.FindAll().Where(y => y.ProcessID == idProcess && y.isShow == true).ToList(),
                    TreatmentWay = _repoTreatmentWay.FindAll().Where(y => y.processID == idProcess).ToList()
                }).ToListAsync();
            return lists;
        }

        Task<List<ScheduleDto>> IECService<ScheduleDto>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<object> Done(int scheduleID)
        {
            var item = _repoScheduleUpdate.FindById(scheduleID);
            item.FinishedStatus = !item.FinishedStatus;
            _repoScheduleUpdate.Update(item);
            return await _repoScheduleUpdate.SaveAll();
        }

        public async Task<object> Approve(int scheduleID, int userid)
        {
            var item = _repoScheduleUpdate.FindById(scheduleID);
            item.ApprovalBy = userid;
            if (item.FinishedStatus == true && item.ApprovalStatus == false)
            {
                item.FinishedStatus = true;
            }
            else
            {
                item.FinishedStatus = !item.FinishedStatus;
            }
            item.ApprovalStatus = !item.ApprovalStatus;
            _repoScheduleUpdate.Update(item);
            return await _repoScheduleUpdate.SaveAll();
        }

        public async Task<bool> EditSchedule(ScheduleUpdateEditDto obj)
        {
            var model = _mapper.Map<SchedulesUpdate>(obj);
            _repoScheduleUpdate.Update(model);
            return await _repoScheduleUpdate.SaveAll();
        }

        public async Task<bool> EditPartSchedule(ScheduleUpdatePartDTO obj)
        {
            var model = _mapper.Map<Part>(obj);
            _repoPart.Update(model);
            // var model = _repo
            return await _repoPart.SaveAll();
        }


    }
}