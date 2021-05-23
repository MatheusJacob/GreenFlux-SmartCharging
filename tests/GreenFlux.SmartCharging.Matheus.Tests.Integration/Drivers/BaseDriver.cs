using FluentAssertions;
using GreenFlux.SmartCharging.Matheus.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Integration
{
    public abstract class BaseDriver
    {
        public HttpClient Client { get; set; }
        public string ApiUrl { get; set; }
        public BaseDriver()
        {
            WebApplicationFactory<Startup> appFactory = new WebApplicationFactory<Startup>();
            Client = appFactory.CreateClient();
            ApiUrl = $"{ Client?.BaseAddress?.AbsoluteUri}api/";
        }

        public StringContent ConvertToJsonData<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public T ConvertToObject<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public async Task<T> ParseFromResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            Func<T> parsedResponse = () => ConvertToObject<T>(content);

            parsedResponse.Should().NotThrow();

            return parsedResponse();
        }
    }

}
