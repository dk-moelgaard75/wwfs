using FuelLog.Models;
using FuelLog.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FuelLog.Services
{
    public class FuelingService : IFuelingService
    {
        private string CreateFuelingUrl { get; set; }
        private string GetFuelingUrl { get; set; }
        private string UpdateFuelingUrl { get; set; }
        private string DeleteFuelingUrl { get; set; }
        public string LastError { get; set; }
        public string LastMessage { get; set; }
        public FuelingService(IOptions<AppSettings> appSetting)
        {
            //CreateUserUrl = appSettings.GetValue<string>("UserCreateURL");
            CreateFuelingUrl = appSetting.Value.FuelingCreateURL;
            GetFuelingUrl = appSetting.Value.FuelingGetURL;
            UpdateFuelingUrl = appSetting.Value.FuelingUpdateURL;
            DeleteFuelingUrl = appSetting.Value.FuelingDeleteURL;
        }
        public async Task AddFuelingAsync(FuelingModel fueling)
        {
            CancellationToken cancellationToken;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, CreateFuelingUrl))
            using (var httpContent = HttpUtil.CreateHttpContent(fueling))
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
        public async Task UpdateFuelingAsync(FuelingModel fueling)
        {
            try
            {
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, UpdateFuelingUrl))
                using (var httpContent = HttpUtil.CreateHttpContent(fueling))
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
        public async Task DeleteFuelingAsync(int uId)
        {
            try
            {
                dynamic content = new { id = uId };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, UpdateFuelingUrl))
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
        public async Task<FuelingModel> GetFuelingAsync(int uId)
        {
            try
            {
                dynamic content = new { id = uId };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetFuelingUrl))
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
                            List<FuelingModel> fueling = JsonConvert.DeserializeObject<List<FuelingModel>>(await response.Content.ReadAsStringAsync());
                            return fueling.First();
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
        public async Task<List<FuelingModel>> GetFuelingsAsync()
        {
            List<FuelingModel> list = new List<FuelingModel>();
            try
            {
                dynamic content = new { };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetFuelingUrl))
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
                            list = JsonConvert.DeserializeObject<List<FuelingModel>>(await response.Content.ReadAsStringAsync());
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

    }
}
