using AutoMapper;
using TestApi.Core.Entity;
using TestApi.DTO;

namespace TestApi.Core.Mapping
{
    public class DtoMapping:Profile
    {
        public DtoMapping()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Organisation, OrganisationDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Registration, GetRegistrationResponse>();
        }
    }
}