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
    public class InkService : IInkService
    {
        private readonly IProcessRepository _repoProcess;
        private readonly ISupplierRepository _repoSup;
        private readonly IInkRepository _repoInk;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public InkService(IProcessRepository repoProcess , ISupplierRepository repoSup ,IInkRepository repoInk , IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoInk = repoInk;
            _repoSup = repoSup;
            _repoProcess = repoProcess;

        }

        private async Task<InkDto> AddInk(InkForImportExcelDto inkDto)
        {
            var result = new InkDto();
          
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
             new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
            
                var supname = await _repoSup.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(inkDto.Supplier.ToUpper()));
                if (supname != null)
                {
                    result.SupplierID = supname.ID;
                }
                else
                {
                }
        
                var process = await _repoProcess.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(inkDto.Process.ToUpper()));
                if (process != null)
                {
                    result.ProcessID = process.ID;
                }
                else
                {
            
                }

                // result.CreatedBy = inkDto.CreatedBy;
                scope.Complete();
                return result;
            }
        }
        public async Task ImportExcel(List<InkForImportExcelDto> inkDtos)
        {
            try
            {
                var list = new List<InkForImportExcelDto>();
                var listChuaAdd = new List<InkForImportExcelDto>();
                var result = inkDtos.DistinctBy(x => new
                {
                    x.Name,
                    x.Process
                }).Where(x => x.Name != "").ToList();

                foreach (var item in result)
                {
                    var supname = await _repoSup.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(item.Supplier.ToUpper()));
                    if (supname != null)
                    {
                        item.SupplierID = supname.ID;
                    }
                     var process = await _repoProcess.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(item.Process.ToUpper()));
                    if (process != null)
                    {
                        item.ProcessID = process.ID;
                    }
                    // var ink = await AddInk(item);
                    list.Add(item);
                }

                var listAdd = new List<Ink>();
                foreach (var ink in list)
                {
                    if (!await CheckExistInk(ink))
                    {
                        var inks = new Ink();
                        inks.SupplierID = ink.SupplierID;
                        inks.Code = ink.Code;
                        inks.Name = ink.Name;
                        inks.ProcessID = ink.ProcessID;
                        inks.MaterialNO = ink.MaterialNO;
                        inks.Unit = ink.Units;
                        inks.CreatedDate = ink.CreatedDate;
                        inks.CreatedBy = ink.CreatedBy;
                        inks.DaysToExpiration = ink.DaysToExpiration;
                        inks.isShow = true;
                        inks.VOC = ink.VOC;
                        _repoInk.Add(inks);
                        await _repoInk.SaveAll();
                        listAdd.Add(inks);
                    }
                    else
                    {
                        listChuaAdd.Add(ink);
                    }
                }
                var result1 = listAdd.Where(x => x.ID > 0).ToList();
                var result2 = listAdd.Where(x => x.ID == 0).ToList();
            }
            catch
            {
                throw;
            }
        }

        private async Task<bool> CheckExistInk(InkForImportExcelDto ink)
        {
            return await _repoInk.FindAll().AnyAsync(x => x.Name == ink.Name && x.ProcessID == ink.ProcessID );
        }
        public async Task<bool> AddRangeAsync(List<InkForImportExcelDto> model)
        {
            var ingredients = _mapper.Map<List<Ink>>(model);
            ingredients.ForEach(ingredient => { ingredient.isShow = true; });
            _repoInk.AddRange(ingredients);
            return await _repoInk.SaveAll();
        }
        public async Task<bool> Add(InkDto model)
        {
            var ink = _mapper.Map<Ink>(model);
            ink.isShow = true;
            ink.CreatedDate = DateTime.Now;
            _repoInk.Add(ink);
            return await _repoInk.SaveAll();
        }

        public async Task<bool> Delete(object id)
        {
            var ink = _repoInk.FindById(id);
            _repoInk.Remove(ink);
            return await _repoInk.SaveAll();
        }

        public async Task<List<InkDto>> GetAllAsync()
        {
            // ProjectTo<InkDto>(_configMapper)
            return await _repoInk.FindAll().Include(x => x.Supplier).Include(x => x.Processes).Select(x => new InkDto {
                ID = x.ID,
                Code = x.Code,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                MaterialNO = x.MaterialNO,
                VOC = x.VOC,
                Unit = x.Unit,
                Supplier = x.Supplier.Name,
                Process = x.Processes.Name,
                DaysToExpiration = x.DaysToExpiration,
                SupplierID = x.SupplierID,
                ProcessID = x.ProcessID,
                Percentage = x.percentage,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate
            }).OrderByDescending(x => x.ID).ToListAsync();
        }

        public InkDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<InkDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<InkDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(InkUpdateDto model)
        {
            var ink = _mapper.Map<Ink>(model);
            _repoInk.Update(ink);
            return await _repoInk.SaveAll();
        }

        public Task<bool> Update(InkDto model)
        {
            throw new NotImplementedException();
        }

        //Thêm Brand mới vào bảng Line
        // public async Task<bool> Add(KindDto model)
        // {
        //     var Line = _mapper.Map<Kind>(model);
        //     _repoLine.Add(Line);
        //     return await _repoLine.SaveAll();
        // }



        // //Lấy danh sách Brand và phân trang
        // public async Task<PagedList<KindDto>> GetWithPaginations(PaginationParams param)
        // {
        //     var lists = _repoLine.FindAll().ProjectTo<KindDto>(_configMapper).OrderByDescending(x => x.ID);
        //     return await PagedList<KindDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        // }

        // //Tìm kiếm Line
        // public async Task<PagedList<KindDto>> Search(PaginationParams param, object text)
        // {
        //     var lists = _repoLine.FindAll().ProjectTo<KindDto>(_configMapper)
        //     .Where(x => x.Name.Contains(text.ToString()))
        //     .OrderByDescending(x => x.ID);
        //     return await PagedList<KindDto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        // }
        // //Xóa Brand
        // public async Task<bool> Delete(object id)
        // {
        //     var Line = _repoLine.FindById(id);
        //     _repoLine.Remove(Line);
        //     return await _repoLine.SaveAll();
        // }

        // //Cập nhật Brand
        // public async Task<bool> Update(KindDto model)
        // {
        //     var Line = _mapper.Map<Kind>(model);
        //     _repoLine.Update(Line);
        //     return await _repoLine.SaveAll();
        // }

        // //Lấy toàn bộ danh sách Brand 
        // public async Task<List<KindDto>> GetAllAsync()
        // {
        //     return await _repoLine.FindAll().ProjectTo<KindDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        // }

        // //Lấy Brand theo Brand_Id
        // public KindDto GetById(object id)
        // {
        //     return  _mapper.Map<Kind, KindDto>(_repoLine.FindById(id));
        // }

    }
}