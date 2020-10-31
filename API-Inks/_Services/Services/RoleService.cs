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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repoRole;
        private readonly IRoleUserRepository _repoRoleUser;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public RoleService(IRoleUserRepository repoRoleUser , IRoleRepository repoRole , IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoRole = repoRole;
            _repoRoleUser = repoRoleUser;

        }

        public Task<bool> Add(RoleDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckLoginByUser(int userid)
        {
            var model = await _repoRoleUser.FindAll().AnyAsync(x => x.UserID == userid && x.Status == true);
            return model ;
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoleDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleDto>> GetAllRole()
        {
            return await _repoRole.FindAll().ProjectTo<RoleDto>(_configMapper).ToListAsync();
            // return await _repoRole.FindAll().ToListAsync();
            // throw new NotImplementedException();

        }

        public async Task<List<RoleUserDto>> GetAllRoleUser()
        {
            return await _repoRoleUser.FindAll().ProjectTo<RoleUserDto>(_configMapper).ToListAsync();
        }

        public RoleDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetRoleByUserID(int userid)
        {
            var model = await _repoRoleUser.FindAll().FirstOrDefaultAsync(x => x.UserID == userid);
            return _repoRole.FindById(model.RoleID);
        }

        public Task<PagedList<RoleDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public async Task<object> LockUser(int userid)
        {
            try
            {
                var model = await _repoRoleUser.FindAll().FirstOrDefaultAsync(x => x.UserID == userid);
                // if (model.Status == true) {
                //     model.Status = false;
                // } else {
                //     model.Status = true;
                // }
                model.Status = !model.Status;
                await _repoRoleUser.SaveAll();
                return new
                {
                    status = true,
                    message = "Lock User Successfully!"
                };
            }
            catch (System.Exception)
            {
                
                return new
                    {
                        status = false,
                        message = "Failed on Block!"
                    };
            }
            
        }

        public async Task<object> MapRoleUser(int userid, int roleID)
        {
            var item = await _repoRoleUser.FindAll().FirstOrDefaultAsync(x => x.UserID == userid);
            if (item == null)
            {
                _repoRoleUser.Add(new RoleUser
                {
                    UserID = userid,
                    RoleID = roleID,
                    Status = true
                });
                try
                {
                    await _repoRoleUser.SaveAll();
                    return new
                    {
                        status = true,
                        message = "Mapping Successfully!"
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        status = false,
                        message = "Failed on save!"
                    };
                }
            }
            else
            {
                item.UserID = userid;
                item.RoleID = roleID;

                try
                {
                    await _repoRoleUser.SaveAll();
                    return new
                    {
                        status = true,
                        message = "Mapping Successfully!"
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        status = false,
                        message = "Failed on save!"
                    };
                }
            }
        }

        public Task<PagedList<RoleDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(RoleDto model)
        {
            throw new NotImplementedException();
        }
    }
}