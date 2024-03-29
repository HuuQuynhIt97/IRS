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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repoSupplier;
        private readonly IProcessRepository _repoProcess;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IHttpContextAccessor _accessor;

        public SupplierService(IProcessRepository repoProcess ,ISupplierRepository repoSupplier,IHttpContextAccessor accessor, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoSupplier = repoSupplier;
            _accessor = accessor;
            _repoProcess = repoProcess;

        }

        //Thêm Supplier mới vào bảng Supplier
        public async Task<bool> Add(SuppilerDto model)
        {
            var Supplier = _mapper.Map<Supplier>(model);
            Supplier.isShow = true;
            Supplier.ProcessID = model.ProcessID;
            _repoSupplier.Add(Supplier);
            return await _repoSupplier.SaveAll();
        }

        //Lấy danh sách Supplier và phân trang
        public async Task<PagedList<SuppilerDto>> GetWithPaginations(PaginationParams param)
        {
            var lists = _repoSupplier.FindAll().Where(x => x.isShow == true).ProjectTo<SuppilerDto>(_configMapper).OrderBy(x => x.ID);
            return await PagedList<SuppilerDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }
      
        //Tìm kiếm Supplier
        public async Task<PagedList<SuppilerDto>> Search(PaginationParams param, object text)
        {
            var lists = _repoSupplier.FindAll().Where(x => x.isShow == true).ProjectTo<SuppilerDto>(_configMapper)
            .Where(x => x.Name.Contains(text.ToString()))
            .OrderBy(x => x.ID);
            return await PagedList<SuppilerDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }

        //Xóa Supplier
        public async Task<bool> Delete(object id)
        {
            string token = _accessor.HttpContext.Request.Headers["Authorization"];
            var userID = JWTExtensions.GetDecodeTokenByProperty(token, "nameid").ToInt();
            if (userID == 0) return false;
            var supplier = _repoSupplier.FindById(id);
            _repoSupplier.Remove(supplier);
            // supplier.isShow = false;
            // supplier.ModifiedBy = userID;
            // supplier.ModifiedDate = DateTime.Now;
            return await _repoSupplier.SaveAll();
        }

        //Cập nhật Supplier
        public async Task<bool> Update(SuppilerDto model)
        {
            string token = _accessor.HttpContext.Request.Headers["Authorization"];
            var userID = JWTExtensions.GetDecodeTokenByProperty(token, "nameid").ToInt();
            if (userID == 0) return false;
            var supplier = _mapper.Map<Supplier>(model);
            supplier.isShow = true;
            supplier.ProcessID = model.ProcessID;
            supplier.ModifiedBy = userID;
            supplier.ModifiedDate = DateTime.Now;
            _repoSupplier.Update(supplier);
            return await _repoSupplier.SaveAll();
        }
      
        //Lấy toàn bộ danh sách Supplier 
        public async Task<List<SuppilerDto>> GetAllAsync()
        {
            // ProjectTo<SuppilerDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync()
            return await _repoSupplier.FindAll().Where(x => x.isShow == true).Select(x => new SuppilerDto {
                ID = x.ID,
                Name = x.Name,
                Process = _repoProcess.FindAll().FirstOrDefault(y => y.ID == x.ProcessID).Name
            }).OrderByDescending(x => x.ID).ToListAsync();
        }

        //Lấy Supplier theo Supplier_Id
        public SuppilerDto GetById(object id)
        {
            return  _mapper.Map<Supplier, SuppilerDto>(_repoSupplier.FindById(id));
        }

        public async Task<object> GetAllWithName()
        {
            return await _repoSupplier.FindAll().Where(x => x.isShow == true).Select(x => new SuppilerDto {
                ID = x.ID,
                Name = x.Name + " - " + _repoProcess.FindAll().FirstOrDefault(y => y.ID == x.ProcessID).Name,
                Process = _repoProcess.FindAll().FirstOrDefault(y => y.ID == x.ProcessID).Name
            }).OrderByDescending(x => x.ID).ToListAsync();
        }

        public async Task<object> GetAllByTreatment(int id)
        {
            return await _repoSupplier.FindAll().Where(x => x.isShow == true && x.ProcessID == id).Select(x => new SuppilerDto {
                ID = x.ID,
                Name = x.Name,
                Process = _repoProcess.FindAll().FirstOrDefault(y => y.ID == x.ProcessID).Name
            }).OrderByDescending(x => x.ID).ToListAsync();
        }
    }
}