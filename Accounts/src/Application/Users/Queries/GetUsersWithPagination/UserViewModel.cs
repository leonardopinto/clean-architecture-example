using Accounts.Application.Common.Mappings;
using Accounts.Domain.Entities;
using AutoMapper;

namespace Accounts.Application.Users.Queries.GetUsersWithPagination;

public class UserViewModel : IMapFrom<User>
{
    public Guid Id { get; set; }

    public string? Name { get; set; }   

    public string? Email { get; set; }

    public void Mapping(Profile profile) 
    {
        profile.CreateMap<User, UserViewModel>();
    }
}
