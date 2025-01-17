using ControllRR.Application.Interfaces;
using ControllRR.Infrastructure.Repositories;
using ControllRR.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Presentation.ViewModels;
using System.Diagnostics;
using ControllRR.Domain.Entities;

namespace ControlRR.Controllers;

public class MaintenancesController : Controller
{

    private readonly IMaintenanceService _maintenanceService;
    private readonly IUserService _userService;
    private readonly IDeviceService _deviceService;
    private readonly ControllRRContext _controllRRContext;
    public MaintenancesController(IMaintenanceService maintenanceService, IUserService userService, IDeviceService deviceService, ControllRRContext controllRRContext)
    {
        _maintenanceService = maintenanceService;
        _userService = userService;
        _deviceService = deviceService;
        _controllRRContext = controllRRContext;
    }

    public async Task<IActionResult> Index()
    {
        var obj = await _maintenanceService.FindAllAsync();
        return View(obj);
    }

    public async Task<IActionResult> Details(int id)
    {
        // PAREI AQUI var obj = new MaintenanceViewModel { Maintenance = }
        var list = await _maintenanceService.FindByIdAsync(id);
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Identificador não informado!" });
        }
        if (list == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Identificador não encontrado!" });
        }
        return View(list);

    }
    [HttpGet]
    public async Task<IActionResult> New()
    {

        var users = await _userService.FindAllAsync();
        var device = await _deviceService.FindAllAsync();
        var viewModel = new MaintenanceViewModel { User = users, Device = device };
        return View(viewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(Maintenance maintenance)
    {
        if (!ModelState.IsValid)
        {
            var user = await _userService.FindAllAsync();
            var device = await _deviceService.FindAllAsync();
            var viewModel = new MaintenanceViewModel { Maintenance = maintenance, User = user, Device = device };


            return View(viewModel);
        }

        await _maintenanceService.InsertAsync(maintenance);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Error(string message)
    {
        var viewModel = new ErrorViewModel
        {
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel);
    }

}
