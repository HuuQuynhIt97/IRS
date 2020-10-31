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
    public class SchedulePartService : ISchedulePartService
    {
        private readonly IPartRepository _repoPart;
        private readonly IInkRepository _repoInk;
        private readonly IGluesRepository _repoGlues;
        private readonly IChemicalRepository _repoChemical;
        private readonly IScheduleRepository _repoSchedule;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public SchedulePartService(IGluesRepository repoGlues,IChemicalRepository repoChemical, IInkRepository repoInk, IScheduleRepository repoSchedule, IPartRepository repoPart, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoPart = repoPart;
            _repoSchedule = repoSchedule;
            _repoInk = repoInk;
            _repoChemical = repoChemical;
            _repoGlues = repoGlues;

        }

        public Task<bool> Add(SchedulePartDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SchedulePartDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public SchedulePartDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<SchedulePartDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<SchedulePartDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(SchedulePartDto model)
        {
            throw new NotImplementedException();
        }
    }
}