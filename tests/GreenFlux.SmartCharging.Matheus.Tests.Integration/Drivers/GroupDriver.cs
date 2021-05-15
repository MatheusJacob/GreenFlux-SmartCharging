using GreenFlux.SmartCharging.Matheus.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace GreenFlux.SmartCharging.Matheus.Tests.Integration.Drivers
{
    public class GroupDriver : BaseDriver
    {
        private string GroupApiUrl;     
        GroupDriver() : base()
        {
            GroupApiUrl = ApiUrl + "groups/";
        }

        public async Task ShouldCreateAGroupSuccessfully(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            GroupResource groupResourceResponse = new GroupResource();
            Action parseAction = () => groupResourceResponse = ConvertToObject<GroupResource>(content);
            
            parseAction.Should().NotThrow();
            response.StatusCode.Should().Be(201);
            groupResourceResponse.Id.Should().NotBeEmpty();
            groupResourceResponse.Name.Should().NotBeEmpty();
            groupResourceResponse.Capacity.Should().BePositive();
        }

        public async Task ShouldNotCreateAGroupSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(400);
            var test = ConvertToObject<dynamic>(await response.Content.ReadAsStringAsync());

            ////Todo validate if the error message is the same as the missing argument
        }

        public async Task<HttpResponseMessage> CreateGroup(string name, float? capacity)
        {
            SaveGroupResource saveGroupResource = new SaveGroupResource()
            {
                Name = name,
                Capacity = capacity
            };

            return await Client.PostAsync(GroupApiUrl, ConvertToJsonData<SaveGroupResource>(saveGroupResource));
        }

     

    }
}
