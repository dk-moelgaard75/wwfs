using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelLog.Models;
using FuelLog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FuelLog.Controllers
{
    public class VehicleController : Controller
    {
        private IUserService _userService = null;
        private IVehicleService _vehicleService = null;
        public VehicleController(IUserService users, IVehicleService vehicles)
        {
            _userService = users;
            _vehicleService = vehicles;
        }

        public async Task<IActionResult> List()
        {
            IEnumerable<VehicleModel> list = new List<VehicleModel>();
            try
            {
                list = await _vehicleService.GetVehiclesAsync();
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }
            return View(list);

        }
        public async Task<IActionResult> Create()
        {
            try
            {
                List<UserModel> list = await _userService.GetUsersAsync();
                ViewData["Users"] = new SelectList(list, "UserId", "GetUserIdentification");
                return View();
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }
            IEnumerable<VehicleModel> model = new List<VehicleModel>();
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModel vehicle)
        {
            try
            {
                await _vehicleService.AddVehicleAsync(vehicle);
                ViewBag.Result = "Vehicle created: " + _vehicleService.LastMessage;
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _vehicleService.LastError;
            }
            //IEnumerable<VehicleModel> model = new List<VehicleModel>();
            VehicleModel model = new VehicleModel();
            return View(model);

        }
        public async Task<ActionResult> Details(int id)
        {
            VehicleModel vehicle = new VehicleModel();
            try
            {
                vehicle = await _vehicleService.GetVehicleAsync(id);
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _vehicleService.LastError;
            }
            return View(vehicle);

        }
        public async Task<ActionResult> Edit(int id)
        {
            VehicleModel vehicle = new VehicleModel();
            try
            {
                vehicle = await _vehicleService.GetVehicleAsync(id);
                ViewBag.Result = _vehicleService.LastMessage;
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _vehicleService.LastError;
            }
            return View(vehicle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, VehicleModel vehicle)
        {
            try
            {
                await _vehicleService.UpdateVehicleAsync(vehicle);
                ViewBag.Result = _vehicleService.LastMessage;
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }
            return View(vehicle);
        }
        public async Task<ActionResult> Delete(int id)
        {
            VehicleModel vehicle = new VehicleModel();
            try
            {
                vehicle = await _vehicleService.GetVehicleAsync(id);
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }

            return View(vehicle);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, VehicleModel vehicle)
        {
            try
            {
                await _vehicleService.DeleteVehicleAsync(id);
                ViewBag.Result = _vehicleService.LastMessage;
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
                return View();
            }
            return Redirect("/Vehicle/List");

        }
    }
}
