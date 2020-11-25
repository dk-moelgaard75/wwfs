using FuelLog.Models;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelLog.Services
{
    public interface IUserService
    {
        Task AddUserAsync(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(int id);
        Task<UserModel> GetUserAsync(int id);
        Task<List<UserModel>> GetUsersAsync();
        Task<List<SelectListItem>> GetUsersAsSelectAsync(string user);

        string LastError { get; set; }
        string LastMessage { get; set; }
    }
}