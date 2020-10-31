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
using System.Text.Json;

namespace INK_API._Services.Services
{
    public class GluesService : IGluesService
    {
        private readonly IPartRepository _repoPart;
        private readonly IInkRepository _repoInk;
        private readonly IGluesRepository _repoGlues;
        private readonly IChemicalRepository _repoChemical;
        private readonly IPartInkChemicalRepository _repoPartInkChemical;
        private readonly IScheduleRepository _repoSchedule;
        private readonly ITreatmentWayRepository _repoTreatmentWay;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public GluesService(ITreatmentWayRepository repoTreatmentWay ,IPartInkChemicalRepository repoPartInkChemical, IGluesRepository repoGlues, IChemicalRepository repoChemical, IInkRepository repoInk, IScheduleRepository repoSchedule, IPartRepository repoPart, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoPart = repoPart;
            _repoTreatmentWay = repoTreatmentWay;
            _repoSchedule = repoSchedule;
            _repoPartInkChemical = repoPartInkChemical;
            _repoInk = repoInk;
            _repoChemical = repoChemical;
            _repoGlues = repoGlues;

        }

        public async Task<bool> Add(GluesDto model)
        {
           
            var glue = _mapper.Map<Glues>(model);
            _repoGlues.Add(glue);
            return await _repoGlues.SaveAll();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GluesDto>> GetAllAsync()
        {
            return await _repoGlues.FindAll().ProjectTo<GluesDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        public GluesDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetGluesByScheduleID(int id)
        {
            var list = await _repoGlues.FindAll().Where(x => x.ScheduleID == id)
            .Select(x => new GluesDto
            {
                ID = x.ID,
                Name = x.Name,
                PartID = x.PartID,
                TreatmentWayID = x.TreatmentWayID,
                CreatedDate = x.CreatedDate,
                Part = _repoPart.FindAll().FirstOrDefault(y => y.ID == x.PartID).Name,
                TreatmentWay = _repoTreatmentWay.FindAll().FirstOrDefault(y => y.ID == x.TreatmentWayID).Name
            }).ToListAsync();
            return list;
        }

        public async Task<bool> Deletes(int id)
        {
            var part = _repoGlues.FindById(id);
            _repoGlues.Remove(part);
            return await _repoGlues.SaveAll();
        }

        public Task<PagedList<GluesDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<GluesDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(GluesDto model)
        {
            var glues = _mapper.Map<Glues>(model);
            _repoGlues.Update(glues);
            return await _repoGlues.SaveAll();
        }

        public async Task<object> SaveGlue(PartInkChemicalDto obj)
        {
            try
            {
                var glues = _repoGlues.FindById(obj.glueID);
                glues.Name = obj.name;
                _repoGlues.Update(glues);

                foreach (var item in obj.listAdd)
                {
                    if (item.subname == "Ink")
                    {

                        var result = _repoPartInkChemical.FindAll().FirstOrDefault(x => x.PartID == obj.partID && x.InkID == item.ID && x.GlueID == obj.glueID);

                        // nếu khác Null thi update lai
                        if (result != null)
                        {
                            result.Percentage = item.percentage;
                            _repoPartInkChemical.Update(result);
                            await _repoPartInkChemical.SaveAll();
                        }
                        else
                        {
                            var model = new PartInkChemical
                            {
                                PartID = obj.partID,
                                InkID = item.ID,
                                GlueID = obj.glueID,
                                Percentage = item.percentage
                            };
                            // var data = _repoPartInkChemical.FindAll();
                            _repoPartInkChemical.Add(model);
                            await _repoPartInkChemical.SaveAll();
                        }
                    }
                    else
                    {

                        var result = _repoPartInkChemical.FindAll().FirstOrDefault(x => x.PartID == obj.partID && x.ChemicalID == item.ID && x.GlueID == obj.glueID);

                        // nếu khác Null thi update lai
                        if (result != null)
                        {
                            result.Percentage = item.percentage;
                            _repoPartInkChemical.Update(result);
                            await _repoPartInkChemical.SaveAll();
                        }
                        else
                        {
                            var model = new PartInkChemical
                            {
                                PartID = obj.partID,
                                ChemicalID = item.ID,
                                GlueID = obj.glueID,
                                Percentage = item.percentage
                            };
                            // var data = _repoPartInkChemical.FindAll();
                            _repoPartInkChemical.Add(model);
                            await _repoPartInkChemical.SaveAll();
                        }

                    }
                }
                // var message = "success";
                return new
                {
                    data = await _repoGlues.SaveAll(),
                    status = true,
                    message = "success"
                };
            }
            catch (System.Exception ex)
            {
                return new
                {
                    status = false,
                    message = "save to error"
                };
            }
        }

        public async Task<object> GetInkChemicalByGlueID(int id)
        {
            var list = new List<InkChemicalDto>();
            var data = await _repoPartInkChemical.FindAll().Where(x => x.GlueID == id).ToListAsync();
            foreach (var item in data)
            {
                if (item.InkID != 0)
                {
                    var inkmodel = await _repoInk.FindAll().Where(x => x.ID == item.InkID).Select(x => new InkChemicalDto
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Subname = "Ink",
                        modify = true,
                        Code = x.Code,
                        percentage = _repoPartInkChemical.FindAll().FirstOrDefault(x => x.InkID == item.InkID && x.GlueID == id).Percentage
                    }).ToListAsync();
                    foreach (var item2 in inkmodel)
                    {
                        list.Add(item2);
                    }
                }
                else
                {
                    var chemicalmodel = await _repoChemical.FindAll().Where(x => x.ID == item.ChemicalID).Select(x => new InkChemicalDto
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Code = x.Code,
                        Subname = "Chemical",
                        modify = x.Modify,
                        percentage = _repoPartInkChemical.FindAll().FirstOrDefault(x => x.ChemicalID == item.ChemicalID && x.GlueID == id).Percentage
                    }).ToListAsync();
                    foreach (var item2 in chemicalmodel)
                    {
                        list.Add(item2);
                    }
                }
            }
            return list;
        }
    }
}