using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INK_API._Services.Interface;
using INK_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INK_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuildingUserController : ControllerBase
    {
        private readonly IBuildingUserService _buildingUserService;
        private readonly IRoleService _roleService;

        public BuildingUserController(IRoleService roleService ,IBuildingUserService buildingUserService)
        {
            _buildingUserService = buildingUserService;
            _roleService = roleService;
        }
        [HttpPost]
        public async Task<IActionResult> MappingUserWithBuilding([FromBody]BuildingUserDto buildingUserDto)
        {
            var result = await _buildingUserService.MappingUserWithBuilding(buildingUserDto);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveBuildingUser([FromBody] BuildingUserDto buildingUserDto)
        {
            var result = await _buildingUserService.RemoveBuildingUser(buildingUserDto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBuildingUsers()
        {
            var result = await _buildingUserService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var result = await _roleService.GetAllRole();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoleUser()
        {
            var result = await _roleService.GetAllRoleUser();
            return Ok(result);
        }

        [HttpPost("{userid}/{roleID}")]
        public async Task<IActionResult> MapRoleUser(int userid, int roleID)
        {
            var result = await _roleService.MapRoleUser(userid, roleID);
            return Ok(result);
        }


        [HttpGet("{userID}")]
        public async Task<IActionResult> GetRoleByUserID(int userID)
        {
            var result = await _roleService.GetRoleByUserID(userID);
            return Ok(result);
        }

        [HttpGet("{userID}")]
        public async Task<IActionResult> CheckLoginByUser(int userID)
        {
            var result = await _roleService.CheckLoginByUser(userID);
            return Ok(result);
        }

        [HttpPost("{userID}")]
        public async Task<IActionResult> LockUser(int userID)
        {
            var result = await _roleService.LockUser(userID);
            return Ok(result);
        }

        [HttpGet("{buildingID}")]
        public async Task<IActionResult> GetBuildingUserByBuildingID(int buildingID)
        {
            var result = await _buildingUserService.GetBuildingUserByBuildingID(buildingID);
            return Ok(result);
        }
        [HttpGet("{userID}")]
        public async Task<IActionResult> GetBuildingByUserID(int userID)
        {
            var result = await _buildingUserService.GetBuildingByUserID(userID);
            return Ok(result);
        }

        
        [HttpGet("{userid}/{buildingid}")]
        public async Task<IActionResult> MapBuildingUser(int userid, int buildingid)
        {
            var result = await _buildingUserService.MapBuildingUser(userid, buildingid);
                return Ok(result);
        }
    }
}
