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

        public async Task ShouldDeleteSuccessfully(HttpResponseMessage response, Guid deletedGroupId)
        {     
            response.StatusCode.Should().Be(204);
            var getGroupResponse = await this.GetGroup(deletedGroupId);
            getGroupResponse.StatusCode.Should().Be(404);
        }

        public async Task ShouldNotDeleteAnything(HttpResponseMessage response, Guid deletedGroupId)
        {
            response.StatusCode.Should().Be(404);
            var getGroupResponse = await this.GetGroup(deletedGroupId);
            getGroupResponse.StatusCode.Should().Be(404);
        }

        public async Task ShouldCreateAGroupSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(201);

            GroupResource groupResourceResponse = await this.ParseGroupFromResponse(response);            
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

         public async Task ShouldUpdateAGroupSuccessfully(HttpResponseMessage response, GroupResource expectedGroupValues)
        {
            response.StatusCode.Should().Be(200);

            GroupResource groupResourceResponse = await this.ParseGroupFromResponse(response);
            groupResourceResponse.Name.Should().Be(expectedGroupValues.Name);
            groupResourceResponse.Capacity.Should().Be(expectedGroupValues.Capacity);
        }

        public async Task ShouldNotUpdateAGroupSuccessfully(HttpResponseMessage response)
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

        public async Task<HttpResponseMessage> UpdateGroup(Guid id, string name, float? capacity)
        {
            SaveGroupResource saveGroupResource = new SaveGroupResource()
            {
                Name = name,
                Capacity = capacity
            };

            return await Client.PatchAsync($"{GroupApiUrl}{id.ToString()}", ConvertToJsonData<SaveGroupResource>(saveGroupResource));
        }

        public async Task<HttpResponseMessage> GetGroup(Guid id)
        {
            var response = await Client.GetAsync($"{GroupApiUrl}{id.ToString()}");

            return response;
        }

        public async Task<HttpResponseMessage> DeleteGroup(Guid id)
        {            
            var response = await Client.DeleteAsync($"{GroupApiUrl}{id.ToString()}");

            return response;
        }

        public async Task<GroupResource> ParseGroupFromResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            Func<GroupResource> groupConverted = () => ConvertToObject<GroupResource>(content);

            groupConverted.Should().NotThrow();

            return groupConverted();           
        }


    }
}
