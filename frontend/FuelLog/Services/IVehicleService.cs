using FuelLog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelLog.Services
{
    public interface IVehicleService
    {
        string LastError { get; set; }
        string LastMessage { get; set; }

        Task AddVehicleAsync(VehicleModel vehicle);
        Task DeleteVehicleAsync(int uId);
        Task<VehicleModel> GetVehicleAsync(int uId);
        Task<List<VehicleModel>> GetVehiclesAsync();
        Task<List<SelectListItem>> GetVehiclesAsSelectAsync(string userId);
        Task UpdateVehicleAsync(VehicleModel vehicle);
    }
}