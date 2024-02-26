using AICook.Model.Dto;
using AutoMapper;

namespace AICook.Model.Profiles;

public class IdentityProfile : Profile
{
	public IdentityProfile()
	{
		CreateMap<User, UserDto>();
		CreateMap<LoginToken, LoginTokenDto>();
	}
}