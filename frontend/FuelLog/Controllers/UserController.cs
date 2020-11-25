using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelLog.Models;
using FuelLog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FuelLog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List()
        {
            try
            {
                var result = await _userService.GetUsersAsync();
                return View(result);
            }
            catch(Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }
            IEnumerable<UserModel> model = new List<UserModel>();
            return View(model);
            
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var result = await _userService.GetUserAsync(id);
                ViewBag.Result = _userService.LastMessage;
                return View(result);
            }
            catch(Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }
            UserModel model = new UserModel();
            return View(model);

        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserModel user) //(IFormCollection collection)
        {
            try
            {
                await _userService.AddUserAsync(user);
                ViewBag.Result = "Message:" + _userService.LastMessage;
                return View();
            }
            catch
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
                return View();
            }

        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var result = await _userService.GetUserAsync(id);
                //ViewBag.Result = _userService.LastMessage;
                return View(result);
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }
            UserModel model = new UserModel();
            return View(model);

        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserModel user)
        {
            try
            {
                await _userService.UpdateUserAsync(user);
                ViewBag.Result = _userService.LastMessage;
                return View(user);
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }
            UserModel model = new UserModel();
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

        // GET: UserController/Delete/5
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
                await _userService.DeleteUserAsync(id);
                ViewBag.Result = _userService.LastMessage;
                return View();
            }
            catch (Exception)
            {
                ViewBag.Result = "An error occured ->" + _userService.LastError;
            }
            return View();
        }
    }
}
