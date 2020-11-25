using FuelLog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelLog.Services
{
    public interface IFuelingService
    {
        string LastError { get; set; }
        string LastMessage { get; set; }

        Task AddFuelingAsync(FuelingModel fueling);
        Task DeleteFuelingAsync(int uId);
        Task<FuelingModel> GetFuelingAsync(int uId);
        Task<List<FuelingModel>> GetFuelingsAsync();
        Task UpdateFuelingAsync(FuelingModel fueling);
    }
}