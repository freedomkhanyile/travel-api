﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Travel.Api.IntegrationTests.Helpers
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _client;
        public HttpClientWrapper(HttpClient client)
        {
            _client = client;
        }
        public HttpClient Client => _client;

        public async Task<T> PostAsync<T>(string url, object body)
        {
            var response = await _client.PostAsync(url, new JsonContent(body));
            
            response.EnsureSuccessStatusCode();
            
            var responseText = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(responseText);

            return data;
        }
        public async Task PostAsync(string url, object body)
        {
            var response = await _client.PostAsync(url, new JsonContent(body));

            response.EnsureSuccessStatusCode();
        }

        public async Task<T> PutAsync<T>(string url, object body)
        {
            var response = await _client.PutAsync(url, new JsonContent(body));

            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(responseText);
            return data;
        }
    }
}
