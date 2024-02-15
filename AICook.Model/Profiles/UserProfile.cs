using AICook.Model.Dto;
using AutoMapper;

namespace AICook.Model.Profiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, UserDto>();
	}
}