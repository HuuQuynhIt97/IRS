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
using Microsoft.AspNetCore.Http;

namespace INK_API._Services.Services
{
    public class TreatmentWayService : ITreatmentWayService
    {
        private readonly ISupplierRepository _repoSupplier;
        private readonly ITreatmentWayRepository _repoTreatmentWay;
        private readonly IProcessRepository _repoProcess;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IHttpContextAccessor _accessor;

        public TreatmentWayService(ITreatmentWayRepository repoTreatmentWay ,IProcessRepository repoProcess ,ISupplierRepository repoSupplier,IHttpContextAccessor accessor, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoSupplier = repoSupplier;
            _repoTreatmentWay = repoTreatmentWay;
            _accessor = accessor;
            _repoProcess = repoProcess;

        }

        public async Task<bool> Add(TreatmentWayDto model)
        {
           var treatmentWay = _mapper.Map<TreatmentWay>(model);
           var processID = _repoProcess.FindAll().FirstOrDefault(x => x.Name == model.process).ID;
            treatmentWay.processID = processID;
            _repoTreatmentWay.Add(treatmentWay);
            return await _repoTreatmentWay.SaveAll();
        }

        public async Task<bool> Delete(object id)
        {
            var treatmentWay = _repoTreatmentWay.FindById(id);
            _repoTreatmentWay.Remove(treatmentWay);
            return await _repoTreatmentWay.SaveAll();
        }

        public async Task<List<TreatmentWayDto>> GetAllAsync()
        {
            return await _repoTreatmentWay.FindAll().Select(x => new TreatmentWayDto {
                ID = x.ID,
                Name = x.Name,
                process = _repoProcess.FindAll().FirstOrDefault(y => y.ID == x.processID).Name
            }).OrderByDescending(x => x.ID).ToListAsync();
        }

        public TreatmentWayDto GetById(object id)
        {
            return  _mapper.Map<TreatmentWay, TreatmentWayDto>(_repoTreatmentWay.FindById(id));
        }

        public Task<PagedList<TreatmentWayDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<TreatmentWayDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(TreatmentWayDto model)
        {
            // string token = _accessor.HttpContext.Request.Headers["Authorization"];
            // var userID = JWTExtensions.GetDecodeTokenByProperty(token, "nameid").ToInt();
            // if (userID == 0) return false;
            var treatmentWay = _mapper.Map<TreatmentWay>(model);
            var processID = _repoProcess.FindAll().FirstOrDefault(x => x.Name == model.process).ID;
            treatmentWay.processID = processID;
            _repoTreatmentWay.Update(treatmentWay);
            return await _repoTreatmentWay.SaveAll();
        }
    }
}