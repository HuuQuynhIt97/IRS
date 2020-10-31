using INK_API.DTO;
using INK_API.Models;
using AutoMapper;
using System.Linq;

namespace INK_API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public EfToDtoMappingProfile()
        {
            CreateMap<User, UserForDetailDto>();

           
            CreateMap<Process, ProcessDto>();
            CreateMap<Part, PartDto>();

            CreateMap<UserDetailDto, UserDetail>();
            CreateMap<Supplier, SuppilerDto>();

            CreateMap<Building, BuildingDto>();
            CreateMap<BuildingUser, BuildingUserDto>();
            CreateMap<Comment, CommentDto>();
            

                // CreateMap<SchedulesUpdate, ScheduleUpdateDto>()
                // .ForMember(d => d.Parts, o => o.MapFrom(x => x.Part));
                
                
            CreateMap<BuildingGlueForCreateDto, BuildingGlue>();

            CreateMap<SettingDTO, Setting>();
            CreateMap<InkDto, Ink>();
            CreateMap<Chemical, ChemicalDto>();
            CreateMap<InkTblObject, InkObjectDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<RoleUser, RoleUserDto>();
            CreateMap<Glues, GluesDto>();
            CreateMap<SchedulePart, SchedulePartDto>();
            CreateMap<PartInkChemical, PartInkChemicalDtos>();
            CreateMap<SchedulesUpdate, ScheduleUpdateDto>();
            CreateMap<SchedulesUpdate, ScheduleUpdateEditDto>();
            CreateMap<Chemical, ChemicalUpdateDto>();
            CreateMap<Ink, InkUpdateDto>();
            CreateMap<Part, ScheduleUpdatePartDTO>();
            CreateMap<TreatmentWay, TreatmentWayDto>();
            // CreateMap<ScheduleDto, Schedules>();

        }

    }
}