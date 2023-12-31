﻿using InMetrics.HttpLibrary.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace HttpLibrary.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IHttpClientFactory _client;

        public BaseRepository(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<bool> Create(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj == null)
                return true;

            request.Content = new StringContent(JsonConvert.SerializeObject(obj)
                , Encoding.UTF8, "application/json");

            var client = _client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                return true;

            return false;
        }

        public async Task<T> CreateReturnObj(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj == null)
                return obj;

            request.Content = new StringContent(JsonConvert.SerializeObject(obj)
                , Encoding.UTF8, "application/json");

            var client = _client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Delete(string url, string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var request = new HttpRequestMessage(HttpMethod.Delete, url + id);

            var client = _client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;

            return false;
        }


        public async Task<T> Get(string url, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + id);
            var client = _client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            return null;
        }
        public async Task<IList<T>> Get(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var client = _client.CreateClient();

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<T>>(content);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;

            }
        }

        public async Task<IList<T>> Get(string url, T obj)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Content = new StringContent(JsonConvert.SerializeObject(obj)
                , Encoding.UTF8, "application/json");

                var client = _client.CreateClient();


                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<T>>(content);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;

            }
        }
        public Task<bool> Update(string url, T obj)
        {
            throw new NotImplementedException();
        }





    }
}
