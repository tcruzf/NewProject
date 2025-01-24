using ControllRR.Application.Dto;
using Microsoft.AspNetCore.Identity;

namespace ControllRR.Application.Interfaces;

public interface IApplicationUserService
{
     Task<bool> AddUserRoleAsync(string email, string role);

     Task<IdentityUser?> GetUserManagerAsync(string email);

     List<IdentityUser?> ListUser();
}