using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;

namespace FuelLogFuelingBackend
{

    public class Fuelings
    {
        private FuelingDataContext _context;
        public Fuelings(FuelingDataContext context)
        {
            _context = context;
        }

        [FunctionName("CreateFueling")]
        public async Task<IActionResult> CreateFueling(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function 'CreateFueling' processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            log.LogInformation("requestBody:" + requestBody);
            FuelingModel fueling = FuelingModelFactory.CreateFuelingModel(data);
            if (!fueling.DataIsValid)
            {
                var result = new ObjectResult(fueling.InvalidReason);
                //according to https://www.bennadel.com/blog/2434-http-status-codes-for-invalid-data-400-vs-422.htm
                //if data is formattede correct but not valid a 422 is the correct answer
                result.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return result;
            }

            await _context.Fuelings.AddAsync(fueling);
            await _context.SaveChangesAsync();

            string responseMessage = "Fueling - milages:" + fueling.Milages + " - amount:" + fueling.FuelAmount + " - date:" + fueling.FuelingDate+" created";
            return new OkObjectResult(responseMessage);
        
        }
        [FunctionName("GetFueling")]
        public async Task<HttpResponseMessage> GetVGetFuelingehicles(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function 'GetFueling' processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            List<FuelingModel> list = null;
            if ((!String.IsNullOrEmpty(requestBody)) && (!String.IsNullOrEmpty((string)data.id)))
            {
                //Get User with ID
                log.LogInformation("GetFueling for ID:" + (int)data.id);
                list = new List<FuelingModel>();
                list.Add(_context.Fuelings.Find((int)data.id));
            }
            else
            {
                list = _context.Fuelings.ToList<FuelingModel>();
            }
            //string responseMessage = "GetUsers";
            var jsonToReturn = JsonConvert.SerializeObject(list);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
        [FunctionName("UpdateFueling")]
        public async Task<IActionResult> UpdateFueling(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function 'UpdateFueling' processed a request.");

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
            var fueling = _context.Fuelings.First(c => c.Id == id);
            if (fueling == null)
            {
                fueling = new FuelingModel() { Id = id };
                _context.Fuelings.Attach(fueling);
            }
            else
            {
                FuelingModelFactory.UpdateUserModel(data, ref fueling);
            }

            _context.Fuelings.Update(fueling);
            await _context.SaveChangesAsync();

            string responseMessage = "Fueling updated";
            return new OkObjectResult(responseMessage);

        }
        [FunctionName("DeleteFueling")]
        public async Task<IActionResult> DeleteFueling(
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
            var vehicle = _context.Fuelings.First(c => c.Id == id);
            if (vehicle == null)
            {
                vehicle = new FuelingModel() { Id = id };
                _context.Fuelings.Attach(vehicle);
            }
            _context.Fuelings.Remove(vehicle);
            await _context.SaveChangesAsync();

            string responseMessage = "Fueling deleted";
            return new OkObjectResult(responseMessage);
        }

    }
}
