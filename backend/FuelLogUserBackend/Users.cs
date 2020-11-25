using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace FuelLogUserBackend
{
    public class Users
    {
        private UserDataContext _context;

        
        public Users(UserDataContext context)
        {
            _context = context;
        }
        

        [FunctionName("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            log.LogInformation("C# HTTP trigger function 'CreateUser' processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            UserModel user = UserModelFactory.CreateUserModel(data);
            if (!user.DataIsValid)
            {
                var result = new ObjectResult(user.InvalidReason);
                //according to https://www.bennadel.com/blog/2434-http-status-codes-for-invalid-data-400-vs-422.htm
                //if data is formattede correct but not valid a 422 is the correct answer
                result.StatusCode = StatusCodes.Status422UnprocessableEntity; 
                return result;
            }
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            string responseMessage = "User: " + user.FirstName + " " + user.LastName + " created";
            return new OkObjectResult(responseMessage);
        }
        //Using GetUsers for both a specific user and all users - depending on whether the user ID is supplied with the request
        [FunctionName("GetUsers")]
        public async Task<HttpResponseMessage> GetUsers(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function 'GetUsers' processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("requestBody:" + requestBody);
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            List<UserModel> list = null;
            if ((!String.IsNullOrEmpty(requestBody)) && (!String.IsNullOrEmpty((string)data.id)))
            {
                //Get User with ID
                log.LogInformation("GetUsers for ID:" + (int)data.id) ;
                list = new List<UserModel>();
                list.Add(_context.Users.Find((int)data.id));
            }
            else
            {
                list = _context.Users.ToList<UserModel>();
            }
            //string responseMessage = "GetUsers";
            var jsonToReturn = JsonConvert.SerializeObject(list);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
            //return new OkObjectResult(responseMessage);
        }
        [FunctionName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function 'UpdateUser' processed a request.");
            
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
            var user = _context.Users.First(c => c.Id == id);
            if (user == null)
            {
                user = new UserModel() { Id = id };
                _context.Users.Attach(user);
            }
            else
            {
                UserModelFactory.UpdateUserModel(data, ref user);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            
            string responseMessage = "User updated";
            return new OkObjectResult(responseMessage);

        }
        [FunctionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser(
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
            var user = _context.Users.First(c => c.Id == id);
            if (user == null)
            {
                user = new UserModel() { Id = id };
                _context.Users.Attach(user);
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            string responseMessage = "DeleteUser";
            return new OkObjectResult(responseMessage);
        }

    }
}
