using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelLog.Models;
using FuelLog.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> List()
        {
            var result = await _fuelingService.GetFuelingsAsync();
            return View(result);
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
                ViewBag.Result = _fuelingService.LastMessage;
                
            }
            catch (Exception)
            {
                ViewBag.Result = _userService.LastError;
            }
            ViewBag.Users = await _userService.GetUsersAsSelectAsync("");
            return View(fueling);
        }
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var result = await _fuelingService.GetFuelingAsync(id);
                ViewBag.Result = _fuelingService.LastMessage;
                return View(result);
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _fuelingService.LastError;
            }
            FuelingModel model = new FuelingModel();
            return View(model);

        }
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var result = await _fuelingService.GetFuelingAsync(id);
                //ViewBag.Result = _userService.LastMessage;
                return View(result);
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _fuelingService.LastError;
            }
            FuelingModel model = new FuelingModel();
            return View(model);

        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, FuelingModel fueling)
        {
            try
            {
                await _fuelingService.UpdateFuelingAsync(fueling);
                ViewBag.Result = _fuelingService.LastMessage;
                return View(fueling);
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _fuelingService.LastError;
            }
            FuelingModel model = new FuelingModel();
            return View(model);
            /*
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            */
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _fuelingService.DeleteFuelingAsync(id);
                ViewBag.Result = _fuelingService.LastMessage;
                return View();
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _fuelingService.LastError;
            }
            return View();
        }
    }
}
