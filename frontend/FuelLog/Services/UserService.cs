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
    public class UserService : IUserService
    {
        private string CreateUserUrl { get; set; }
        private string GetUserUrl { get; set; }
        private string UpdateUserUrl { get; set; }
        private string DeleteUserUrl { get; set; }
        public string LastError { get; set; }
        public string LastMessage { get; set; }
        public UserService(IOptions<AppSettings> appSetting)
        {
            //CreateUserUrl = appSettings.GetValue<string>("UserCreateURL");
            CreateUserUrl = appSetting.Value.UserCreateURL;
            GetUserUrl = appSetting.Value.UserGetURL;
            UpdateUserUrl = appSetting.Value.UserUpdateURL;
            DeleteUserUrl = appSetting.Value.UserDeleteURL;
        }
        public async Task AddUserAsync(UserModel user)
        {
            //dynamic content = new {};

            CancellationToken cancellationToken;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, CreateUserUrl))
            using (var httpContent = HttpUtil.CreateHttpContent(user))
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
        public async Task UpdateUserAsync(UserModel user)
        {
            try
            {
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, UpdateUserUrl))
                using (var httpContent = HttpUtil.CreateHttpContent(user))
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
        public async Task DeleteUserAsync(int uId)
        {
            try
            {
                dynamic content = new { id = uId };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, UpdateUserUrl))
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
        public async Task<UserModel> GetUserAsync(int uId)
        {
            try
            {
                dynamic content = new { id = uId };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetUserUrl))
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
                            List<UserModel> user = JsonConvert.DeserializeObject<List<UserModel>>(await response.Content.ReadAsStringAsync());
                            return user.First();
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

        public async Task<List<UserModel>> GetUsersAsync()
        {
            List<UserModel> list = new List<UserModel>();
            try
            {
                dynamic content = new { };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetUserUrl))
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
                            list = JsonConvert.DeserializeObject<List<UserModel>>(await response.Content.ReadAsStringAsync());
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
        public async Task<List<SelectListItem>> GetUsersAsSelectAsync(string user = "")
        {
            List<UserModel> list = new List<UserModel>();
            try
            {
                dynamic content = new { };
                CancellationToken cancellationToken;
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, GetUserUrl))
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
                            list = JsonConvert.DeserializeObject<List<UserModel>>(await response.Content.ReadAsStringAsync());
                            return ConverterUtil.GetUsersAsSelect(list,user);
                            //return list;
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
