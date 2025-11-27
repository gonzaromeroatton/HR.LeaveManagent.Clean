using AutoMapper;
using HR.LeaveManagement.BlazorUI.Modules;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.MappingProfiles;

/// <summary>
/// 
/// </summary>
public sealed class MappingConfig : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
    }
}
