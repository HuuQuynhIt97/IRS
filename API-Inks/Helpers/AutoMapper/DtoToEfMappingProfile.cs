using INK_API.DTO;
using INK_API.Models;
using AutoMapper;
using System;

namespace INK_API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        
        public DtoToEfMappingProfile()
        {
            CreateMap<UserForDetailDto, User>();


            CreateMap<GluesDto, Glues>();
            CreateMap<SchedulePartDto, SchedulePart>();
            CreateMap<PartInkChemicalDtos, PartInkChemical>();


            CreateMap<UserDetail, UserDetailDto>();
            CreateMap<SuppilerDto, Supplier>();

            CreateMap<BuildingDto, Building>();
            CreateMap<BuildingUserDto, BuildingUser>();
            CreateMap<CommentDto, Comment>();

            CreateMap<ProcessDto, Process>();
            CreateMap<PartDto, Part>();
            CreateMap<Setting, SettingDTO>();
            CreateMap<Ink, InkDto>();
            CreateMap<ChemicalDto, Chemical>();
            CreateMap<ScheduleDto, Schedules>();
            CreateMap<InkObjectDto, InkTblObject>();
            CreateMap<RoleDto, Role>();
            CreateMap<RoleUserDto, RoleUser>();
            CreateMap<ScheduleDetailDto, Schedules>();
            CreateMap<ScheduleUpdateDto, SchedulesUpdate>();
            CreateMap<ScheduleUpdateEditDto, SchedulesUpdate>();
            CreateMap<ChemicalUpdateDto, Chemical>();
            CreateMap<InkUpdateDto, Ink>();
            CreateMap<ScheduleUpdatePartDTO, Part>();
            CreateMap<TreatmentWayDto, TreatmentWay>();
            //CreateMap<AuditTypeDto, MES_Audit_Type_M>();
        }
    }
}