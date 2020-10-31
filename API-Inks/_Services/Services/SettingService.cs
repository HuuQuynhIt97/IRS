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

namespace INK_API._Services.Services
{
    public class SettingService : ISettingService
    {

        private readonly ISettingRepository _repoSetting;
        private readonly IMapper _mapper;
        public SettingService( IMapper mapper , ISettingRepository repoSetting)
        {
            _repoSetting = repoSetting ;

            _mapper = mapper ;
        }

        public async Task<object> GetAllAsync()
        {
            return await _repoSetting.FindAll().ToListAsync();

        }

        
       
    }
}