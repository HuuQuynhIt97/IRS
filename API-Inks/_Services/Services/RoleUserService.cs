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
    public class RoleUserService : IRoleUserService
    {
        private readonly IProcessRepository _repoProcess;
        private readonly ISupplierRepository _repoSup;
        private readonly IInkRepository _repoInk;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public RoleUserService(IProcessRepository repoProcess , ISupplierRepository repoSup ,IInkRepository repoInk, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoInk = repoInk;
            _repoSup = repoSup;
            _repoProcess = repoProcess;

        }

        public Task<bool> Add(RoleUserDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoleUserDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public RoleUserDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<RoleUserDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<RoleUserDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(RoleUserDto model)
        {
            throw new NotImplementedException();
        }
    }
}