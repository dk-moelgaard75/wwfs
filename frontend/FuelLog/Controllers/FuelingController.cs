using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelLog.Models;
using FuelLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelLog.Controllers
{
    public class FuelingController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IUserService _userService;
        private readonly IFuelingService _fuelingService;
        public  FuelingController(IUserService userService, IVehicleService vehicleService, IFuelingService fuelingService)
        {
            _vehicleService = vehicleService;
            _userService = userService;
            _fuelingService = fuelingService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            List<FuelingModel> model = new List<FuelingModel>();
            return View(model);
        }
        public async Task<IActionResult> GetVehicles(string user)
        {
            try
            {
                ViewBag.Users = await _userService.GetUsersAsSelectAsync(user);
                ViewBag.Vehicles = await _vehicleService.GetVehiclesAsSelectAsync(user);
                ViewBag.StateEnabled = true;
            }
            catch (Exception)
            {
                ViewBag.Result = _userService.LastError;
            }

            return View("Create");
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Users = await _userService.GetUsersAsSelectAsync("");
                ViewBag.StateEnabled = false;
            }
            catch(Exception)
            {
                ViewBag.Result = _userService.LastError;
            }
            FuelingModel model = new FuelingModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuelingModel fueling)
        {
            try
            {
                await _fuelingService.AddFuelingAsync(fueling);
                ViewBag.Result = _userService.LastMessage;
            }
            catch (Exception)
            {
                ViewBag.Result = _userService.LastError;
            }
            return View(fueling);
        }
    }

}
