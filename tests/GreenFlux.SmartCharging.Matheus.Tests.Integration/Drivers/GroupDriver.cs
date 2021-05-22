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

        public void ShouldDeleteSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(204);
        }

        public void ShouldNotFindTheGroup(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(404);
        }

        public async Task ShouldCreateAGroupSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(201);

            GroupResource groupResourceResponse = await this.ParseFromResponse<GroupResource>(response);            
            groupResourceResponse.Id.Should().NotBeEmpty();
            groupResourceResponse.Name.Should().NotBeEmpty();
            groupResourceResponse.Capacity.Should().BePositive();
        }

        public async Task ShouldNotCreateAGroupSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(400);
        }

         public async Task ShouldUpdateAGroupSuccessfully(HttpResponseMessage response, GroupResource expectedGroupValues)
        {
            response.StatusCode.Should().Be(200);

            GroupResource groupResourceResponse = await this.ParseFromResponse<GroupResource>(response);
            if(!string.IsNullOrEmpty(expectedGroupValues.Name))
                groupResourceResponse.Name.Should().Be(expectedGroupValues.Name);

            if (expectedGroupValues.Capacity.HasValue)
                groupResourceResponse.Capacity.Should().Be(expectedGroupValues.Capacity);
        }

        public async Task ShouldNotUpdateAGroupSuccessfully(HttpResponseMessage response)
        {
            var test = ConvertToObject<dynamic>(await response.Content.ReadAsStringAsync());

            response.StatusCode.Should().Match<int>(m => m == 404 || m == 400);
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

        public async Task<HttpResponseMessage> CreateGroupWithUnknowProperty(string name, float? capacity, string extraProperty)
        {
            SaveGroupResource saveGroupResource = new SaveGroupResource()
            {
                Name = name,
                Capacity = capacity
            };

            var serializedGroupPayload = ConvertToJsonData<dynamic>(saveGroupResource);
            Dictionary<string,string> group = ConvertToObject<Dictionary<string, string>>(await serializedGroupPayload.ReadAsStringAsync());
            group.Add(extraProperty, "wrong value");
            var test = ConvertToJsonData<IDictionary<string, string>>(group);
            return await Client.PostAsync(GroupApiUrl, ConvertToJsonData<IDictionary<string,string>>(group));
        }

        public async Task<HttpResponseMessage> UpdateGroup(Guid id, string name, float? capacity)
        {
            PatchGroupResource saveGroupResource = new PatchGroupResource()
            {
                Name = name,
                Capacity = capacity
            };

            return await Client.PatchAsync($"{GroupApiUrl}{id.ToString()}", ConvertToJsonData<PatchGroupResource>(saveGroupResource));
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

        public async Task ShouldNotExistAnymore(Guid groupId)
        {
            HttpResponseMessage groupResponse = await GetGroup(groupId);
            groupResponse.StatusCode.Should().Be(404);
        }
    }
}
