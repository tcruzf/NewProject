using AutoMapper;
using ControllRR.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Application.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMapper _mapper;

    public ApplicationUserService(UserManager<IdentityUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

     public async Task<bool> AddUserRoleAsync(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false; // Usuário não encontrado
        }

        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<IdentityUser?> GetUserManagerAsync(string email)
    {
       return  await _userManager.FindByEmailAsync(email);
        
    }

    public List<IdentityUser?> ListUser()
    {
        return _userManager.Users.ToList();
    }
}