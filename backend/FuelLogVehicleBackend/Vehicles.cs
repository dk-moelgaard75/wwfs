using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Linq;

namespace FuelLogVehicleBackend
{
    public class Vehicles
    {
        private VehicleDataContext _context;

        public Vehicles(VehicleDataContext context)
        {
            _context = context;
        }
        [FunctionName("CreateVehicle")]
        public async Task<IActionResult> CreateVehicle(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function 'CreateVehicle' processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            log.LogInformation("requestBody:" + requestBody);
            VehicleModel vehicle = VehicleModelFactory.CreateVehicleModel(data);
            if (!vehicle.DataIsValid)
            {
                var result = new ObjectResult(vehicle.InvalidReason);
                //according to https://www.bennadel.com/blog/2434-http-status-codes-for-invalid-data-400-vs-422.htm
                //if data is formattede correct but not valid a 422 is the correct answer
                result.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return result;
            }

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            string responseMessage = "Vehicle:" + vehicle.VehicleModelName + " (" + vehicle.LicensePlate + ") created";
            return new OkObjectResult(responseMessage);
        }
        [FunctionName("GetVehicles")]
        public async Task<HttpResponseMessage> GetVehicles(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function 'GetVehicles' processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            List<VehicleModel> list = null;
            if ((!String.IsNullOrEmpty(requestBody)) && (!String.IsNullOrEmpty((string)data.id)))
            {
                //Get User with ID
                log.LogInformation("GetVehicles for ID:" + (int)data.id);
                list = new List<VehicleModel>();
                list.Add(_context.Vehicles.Find((int)data.id));
            }
            else if ((!String.IsNullOrEmpty(requestBody)) && (!String.IsNullOrEmpty((string)data.userid)))
            {
                log.LogInformation("GetVehicles for userId:" + (string)data.userid);
                Guid guid = Guid.Parse((string)data.userid);
                list = new List<VehicleModel>();
                list.AddRange(_context.Vehicles.Where(c => c.UserId == guid));
            }
            else
            {
                list = _context.Vehicles.ToList<VehicleModel>();
            }
            //string responseMessage = "GetUsers";
            var jsonToReturn = JsonConvert.SerializeObject(list);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
        [FunctionName("UpdateVehicle")]
        public async Task<IActionResult> UpdateVehicle(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function 'UpdateVehicle' processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if (String.IsNullOrEmpty(requestBody))
            {
                var result = new ObjectResult("Empty requestbody recieved");
                //according to https://www.bennadel.com/blog/2434-http-status-codes-for-invalid-data-400-vs-422.htm
                //if data is formattede correct but not valid a 422 is the correct answer
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }
            //UserModel user = UserModelFactory.UpdateUserModel(data);
            int id = (int)data.id;
            var vehicle = _context.Vehicles.First(c => c.Id == id);
            if (vehicle == null)
            {
                vehicle = new VehicleModel() { Id = id };
                _context.Vehicles.Attach(vehicle);
            }
            else
            {
                VehicleModelFactory.UpdateUserModel(data, ref vehicle);
            }

            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();

            string responseMessage = "Vehicle updated";
            return new OkObjectResult(responseMessage);

        }
        [FunctionName("DeleteVehicle")]
        public async Task<IActionResult> DeleteVehicle(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if (String.IsNullOrEmpty(requestBody))
            {
                var result = new ObjectResult("Empty requestbody recieved");
                //according to https://www.bennadel.com/blog/2434-http-status-codes-for-invalid-data-400-vs-422.htm
                //if data is formattede correct but not valid a 422 is the correct answer
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }
            int id = (int)data.id;
            var vehicle = _context.Vehicles.First(c => c.Id == id);
            if (vehicle == null)
            {
                vehicle = new VehicleModel() { Id = id };
                _context.Vehicles.Attach(vehicle);
            }
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            string responseMessage = "Vehicle deleted";
            return new OkObjectResult(responseMessage);
        }

    }
}
