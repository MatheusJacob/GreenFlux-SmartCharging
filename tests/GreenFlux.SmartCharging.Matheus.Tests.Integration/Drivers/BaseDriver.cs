using GreenFlux.SmartCharging.Matheus.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
  
}
