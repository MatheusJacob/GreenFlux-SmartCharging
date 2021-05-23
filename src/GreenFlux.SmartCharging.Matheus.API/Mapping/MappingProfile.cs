using AutoMapper;
using GreenFlux.SmartCharging.Matheus.API.Resources;
using GreenFlux.SmartCharging.Matheus.Domain.Models;

namespace GreenFlux.SmartCharging.Matheus.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Group, GroupResource>();
            CreateMap<GroupResource, Group>();

            CreateMap<Group, SaveGroupResource>();
            CreateMap<SaveGroupResource, Group>();

            CreateMap<PatchGroupResource, Group>();


            CreateMap<SaveChargeStationResource, ChargeStation>();
            CreateMap<ChargeStation, ChargeStationResource>();

            CreateMap<SaveConnectorResource, Connector>();
            CreateMap<Connector, ConnectorResource>();
        }
    }
}
