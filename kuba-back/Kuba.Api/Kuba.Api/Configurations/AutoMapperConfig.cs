using AutoMapper;
using Kuba.Api.Dtos.Incident;
using Kuba.Domain.Models;

namespace Kuba.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Incident, IncidentCreateRequest>().ReverseMap();
            CreateMap<Incident, IncidentUpdateRequest>().ReverseMap();
            CreateMap<Incident, IncidentResponse>().ReverseMap();
        }
    }
}
