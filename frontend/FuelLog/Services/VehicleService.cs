using FuelLog.Models;
using FuelLog.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FuelLog.Services
{
    public class VehicleService : IVehicleService
    {
        private string CreateVehicleUrl { get; set; }
        private string GetVehicleUrl { get; set; }
        private string UpdateVehicleUrl { get; set; }
        private string DeleteVehicleUrl { get; set; }
        public string LastError { get; set; }
        public string LastMessage { get; set; }
        public VehicleService(IOptions<AppSettings> appSetting)
        {
            CreateVehicleUrl = appSetting.Value.VehicleCreateURL;
            GetVehicleUrl = appSetting.Value.VehicleGetURL;
            UpdateVehicleUrl = appSetting.Value.VehicleUpdateURL;
            DeleteVehicleUrl = appSetting.Value.VehicleDeleteURL;
        }
        public async Task AddVehicleAsync(VehicleModel vehicle)
        {
            //dynamic content = new {};

            CancellationToken cancellationToken;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, CreateVehicleUrl))
            using (var httpContent = HttpUtil.CreateHttpContent(vehicle))
            {
                request.Content = httpContent;

                using (var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false))
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        LastError = "Error:" + response.StatusCode;
                        throw new Exception();
                    }
                    else
                    {
                        //var serviceResponse = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());
                        LastMessage = await response.Content.ReadAsStringAsync();
                        //LastMessage = serviceResponse.ToString();
                    }
                }
            }
        }
        public async Task UpdateVehicleAsync(VehicleModel vehicle)
        {
            try
            {
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, UpdateVehicleUrl))
                using (var httpContent = HttpUtil.CreateHttpContent(vehicle))
                {
                    request.Content = httpContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            LastError = "Error:" + response.StatusCode;
                            throw new Exception();
                        }
                        else
                        {
                            LastMessage = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = "Error:" + ex.Message;
                throw new Exception();
            }
            return;
        }
        public async Task DeleteVehicleAsync(int uId)
        {
            try
            {
                dynamic content = new { id = uId };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, DeleteVehicleUrl))
                using (var httpContent = HttpUtil.CreateHttpContent(content))
                {
                    request.Content = httpContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            LastError = "Error:" + response.StatusCode;
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = "Error:" + ex.Message;
                throw new Exception();
            }
            return;

        }
        public async Task<VehicleModel> GetVehicleAsync(int uId)
        {
            try
            {
                dynamic content = new { id = uId };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetVehicleUrl))
                using (var httpContent = HttpUtil.CreateHttpContent(content))
                {
                    request.Content = httpContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            LastError = "Error:" + response.StatusCode;
                            throw new Exception();
                        }
                        else
                        {
                            List<VehicleModel> vehicle = JsonConvert.DeserializeObject<List<VehicleModel>>(await response.Content.ReadAsStringAsync());
                            return vehicle.First();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = "Error:" + ex.Message;
                throw new Exception();
            }

        }
        public async Task<List<VehicleModel>> GetVehiclesAsync()
        {
            List<VehicleModel> list = new List<VehicleModel>();
            try
            {
                dynamic content = new { };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetVehicleUrl))
                using (var httpContent = HttpUtil.CreateHttpContent(content))
                {
                    request.Content = httpContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            LastError = "Error:" + response.StatusCode;
                            throw new Exception();
                        }
                        else
                        {
                            list = JsonConvert.DeserializeObject<List<VehicleModel>>(await response.Content.ReadAsStringAsync());
                            return list;
                            //LastMessage = await response.Content.ReadAsStringAsync();
                            //LastMessage = serviceResponse.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = "Error:" + ex.Message;
                throw new Exception();
            }
        }

        public async Task<List<VehicleModel>> GetVehiclesAsync(string userId)
        {
            List<VehicleModel> list = new List<VehicleModel>();
            try
            {
                dynamic content = new { userid = userId };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetVehicleUrl))
                using (var httpContent = HttpUtil.CreateHttpContent(content))
                {
                    request.Content = httpContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            LastError = "Error:" + response.StatusCode;
                            throw new Exception();
                        }
                        else
                        {
                            list = JsonConvert.DeserializeObject<List<VehicleModel>>(await response.Content.ReadAsStringAsync());
                            return list;
                            //LastMessage = await response.Content.ReadAsStringAsync();
                            //LastMessage = serviceResponse.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = "Error:" + ex.Message;
                throw new Exception();
            }
        }
        public async Task<List<SelectListItem>> GetVehiclesAsSelectAsync(string userId)
        {
            List<VehicleModel> list = new List<VehicleModel>();
            try
            {
                dynamic content = new { userid = userId };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetVehicleUrl))
                using (var httpContent = HttpUtil.CreateHttpContent(content))
                {
                    request.Content = httpContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            LastError = "Error:" + response.StatusCode;
                            throw new Exception();
                        }
                        else
                        {
                            list = JsonConvert.DeserializeObject<List<VehicleModel>>(await response.Content.ReadAsStringAsync());
                            return ConverterUtil.GetVehiclesAsSelect(list);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = "Error:" + ex.Message;
                throw new Exception();
            }
        }

    }

}
