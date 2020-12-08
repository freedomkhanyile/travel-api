using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Travel.Api.IntegrationTests.Helpers;
using Travel.Api.Models.Accounts;
using Travel.Api.Models.Users;

namespace Travel.Api.IntegrationTests.Common
{
    public class ApiServer : IDisposable
    {
        public const string email = "test@email.com";
        public const string Password = "test";

        private IConfigurationRoot _config;
        public HttpClient Client { get; private set; }
        public TestServer Server { get; private set; }
        public ApiServer()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = GetAuthenticatedClient(email, Password); 

        }

        public HttpClient GetAuthenticatedClient(string email, string password)
        {
            var client = Server.CreateClient();
            var response = client.PostAsync("/api/Account/Authenticate",
                new JsonContent(new LoginModel {  Email = email, Password = password })).Result;
            
            response.EnsureSuccessStatusCode();

            var data = JsonConvert.DeserializeObject<UserWithTokenModel>(response.Content.ReadAsStringAsync().Result);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+data.Token);

            return client;
        }

        public void Dispose()
        {
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }

            if (Server != null)
            {
                Server.Dispose();
                Server = null;
            }
        }
    }
}
