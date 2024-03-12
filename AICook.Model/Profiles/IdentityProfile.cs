using AICook.Model.Dto;
using AutoMapper;

namespace AICook.Model.Profiles;

public class IdentityProfile : Profile
{
	public IdentityProfile()
	{
		CreateMap<User, UserDto>();
		CreateMap<UserUpdateDto, User>()
			.ForAllMembers(
				opts => 
					opts.Condition((_, _, srcMember) => srcMember != null)
			);
		
		CreateMap<LoginToken, LoginTokenDto>();
	}
}