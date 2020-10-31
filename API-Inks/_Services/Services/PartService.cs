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
    public class PartService : IPartService
    {
        private readonly IPartRepository _repoPart;
        private readonly IInkRepository _repoInk;
        private readonly IChemicalRepository _repoChemical;
        private readonly IScheduleRepository _repoSchedule;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public PartService(IChemicalRepository repoChemical, IInkRepository repoInk, IScheduleRepository repoSchedule, IPartRepository repoPart, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoPart = repoPart;
            _repoSchedule = repoSchedule;
            _repoInk = repoInk;
            _repoChemical = repoChemical;

        }

        public async Task<bool> Add(PartDto model)
        {
            var part = _mapper.Map<Part>(model);
            if (model.Name== "Who" || model.Name == "who") {
                part.Status = true ;
            }
            _repoPart.Add(part);
            await _repoPart.SaveAll();

            return await _repoSchedule.SaveAll();
        }

        public async Task<bool> Deletes(int id)
        {
            var part = _repoPart.FindById(id);
            _repoPart.Remove(part);
            // var schedule = _repoSchedule.FindAll().FirstOrDefault(x => x.PartID == id);
            // _repoSchedule.Remove(schedule);
            return await _repoSchedule.SaveAll();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartDto>> GetAllAsync()
        {
            var lists = await _repoPart.FindAll().ProjectTo<PartDto>(_configMapper).OrderBy(x => x.Name).ToListAsync();
            return lists;
        }

        public PartDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<PartDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<PartDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(PartDto model)
        {
            var part = _mapper.Map<Part>(model);
            _repoPart.Update(part);
            return await _repoPart.SaveAll();
        }

        public async Task<object> UpdatePart(PartInkChemicalDto obj)
        {
            try
            {
                var part = _repoPart.FindById(obj.partID);
                part.Name = obj.name;
                _repoPart.Update(part);

                foreach (var item in obj.listAdd)
                {
                    if (item.subname == "Ink")
                    {
                        var ink = _repoInk.FindById(item.ID);
                        ink.percentage = item.percentage;
                        _repoInk.Update(ink);
                        await _repoInk.SaveAll();
                    }
                    else
                    {
                        var chemical = _repoChemical.FindById(item.ID);
                        chemical.Percentage = item.percentage;
                        _repoChemical.Update(chemical);
                        await _repoChemical.SaveAll();
                    }
                }
                // var message = "success";
                return new {
                   data = await _repoPart.SaveAll(),
                   status = true,
                   message = "success"
                };
            }
            catch (System.Exception ex)
            {
               return new {
                   status = false,
                   message = "save to error"
                };
            }

        }
    }
}