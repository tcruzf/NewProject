using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class RolesController : Controller
{
    private readonly IApplicationUserService _applicationUserService;

    public RolesController(IApplicationUserService applicationUserService)
    {
        _applicationUserService = applicationUserService;
    }

   public async Task<IActionResult> ListUser()
   {
        throw new NotImplementedException();
        
   }

    [HttpGet]
    public async Task<IActionResult> AlterUser(string email)
    {
        var users = await _applicationUserService.GetUserManagerAsync(email);
        return View(users);
    }

     [HttpPost]
    public async Task<IActionResult> AlterUser(string email, string role)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
        {
            return BadRequest("Email e role são obrigatórios.");
        }

        var result = await _applicationUserService.AddUserRoleAsync(email, role);
        if (result)
        {
            return Ok($"Role '{role}' adicionada ao usuário '{email}' com sucesso.");
        }

        return NotFound("Usuário não encontrado ou erro ao adicionar role.");
    }

}