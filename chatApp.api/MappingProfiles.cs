using AutoMapper;
using chatApp.Dtos;
using chatApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace issuetracker.Api.Helpers;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    // CreateMap<Project, ProjectDto>();
    CreateMap<AppUser, UserDto>();

    // CreateMap<Issue, IssueDto>()
    // .ForMember(issueDto => issueDto.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(Status), src.Status)))
    // .ForMember(issueDto => issueDto.ProjectName, opt => opt.MapFrom(src => src.Project.Name));
  }
}