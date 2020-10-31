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
    public class ChemicalService : IChemicalService
    {
        private readonly IProcessRepository _repoProcess;
        private readonly IChemicalRepository _repoChemical;
        private readonly ISupplierRepository _repoSup;
        private readonly IInkRepository _repoInk;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ChemicalService(IChemicalRepository repoChemical , IProcessRepository repoProcess , ISupplierRepository repoSup ,IInkRepository repoInk, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoInk = repoInk;
            _repoSup = repoSup;
            _repoProcess = repoProcess;
            _repoChemical = repoChemical;

        }

        private async Task<ChemicalDto> AddInk(ChemicalForImportExcelDto chemicalDto)
        {
            var result = new ChemicalDto();
          
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
             new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
            
                var supname = await _repoSup.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(chemicalDto.Supplier.ToUpper()));
                if (supname != null)
                {
                    result.SupplierID = supname.ID;
                }
                else
                {
                }
        
                var process = await _repoProcess.FindAll().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(chemicalDto.Process.ToUpper()));
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
        public async Task ImportExcel(List<ChemicalForImportExcelDto> chemicalDtos)
        {
            try
            {
                var list = new List<ChemicalForImportExcelDto>();
                var listChuaAdd = new List<ChemicalForImportExcelDto>();
                var result = chemicalDtos.DistinctBy(x => new
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

                var listAdd = new List<Chemical>();
                foreach (var ink in list)
                {
                    if (!await CheckExistInk(ink))
                    {
                        var inks = new Chemical();
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
                        inks.Modify = ink.Modify;
                        if(ink.Modify == false) {
                            inks.Percentage = 100 ;
                        }
                        _repoChemical.Add(inks);
                        await _repoChemical.SaveAll();
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

        private async Task<bool> CheckExistInk(ChemicalForImportExcelDto ink)
        {
            return await _repoChemical.FindAll().AnyAsync(x => x.Name == ink.Name && x.ProcessID == ink.ProcessID);
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
            var chemical = _mapper.Map<Chemical>(model);
            if(model.Modify == false) {
                chemical.Percentage = 100 ;
            }
            chemical.isShow = true;
            chemical.CreatedDate = DateTime.Now;
            _repoChemical.Add(chemical);
            return await _repoChemical.SaveAll();
        }

        public async Task<bool> Delete(object id)
        {
            var chemical = _repoChemical.FindById(id);
            _repoChemical.Remove(chemical);
            return await _repoChemical.SaveAll();
            // throw new NotImplementedException();
        }

        public async Task<List<ChemicalDto>> GetAllAsync()
        {
            // ProjectTo<InkDto>(_configMapper)
            return await _repoChemical.FindAll().Include(x => x.Supplier).Include(x => x.Processes).Select(x => new ChemicalDto {
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
                Modify = x.Modify,
                SupplierID = x.SupplierID,
                ProcessID = x.ProcessID,
                
            }).OrderByDescending(x => x.ID).ToListAsync();
        }
        public async Task<object> GetChemicalBySupplier(int id)
        {
            // throw new NotImplementedException();
            var list = new List<InkChemicalDto>();
            var inkmodel = await _repoInk.FindAll().Where(x => x.SupplierID == id).Select(x => new InkChemicalDto {
                ID = x.ID,
                Name = x.Name,
                Subname = "Ink",
                percentage = x.percentage,
                modify = true,
                Code = x.Code

            }).ToListAsync();
            foreach (var item in inkmodel)
            {
                list.Add(item);
            }
            // list.Add(inkmodel);
            var chemicalmodel = await _repoChemical.FindAll().Where(x => x.SupplierID == id).Select(x => new InkChemicalDto {
                ID = x.ID,
                Name = x.Name,
                Subname = "Chemical",
                percentage = x.Percentage,
                modify = x.Modify,
                Code = x.Code
            }).ToListAsync();
            foreach (var item in chemicalmodel)
            {
                list.Add(item);
            }
            return list;
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

        public Task<bool> Update(InkDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ChemicalUpdateDto model)
        {
             var chemical = _mapper.Map<Chemical>(model);
             chemical.CreatedDate = DateTime.Now;
            _repoChemical.Update(chemical);
            return await _repoChemical.SaveAll();
        }

        Task<PagedList<ChemicalDto>> IECService<ChemicalDto>.GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        Task<PagedList<ChemicalDto>> IECService<ChemicalDto>.Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        ChemicalDto IECService<ChemicalDto>.GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ChemicalDto model)
        {
            throw new NotImplementedException();
        }
    }
}