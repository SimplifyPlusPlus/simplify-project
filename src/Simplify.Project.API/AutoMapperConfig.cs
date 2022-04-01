using Simplify.Project.API.Contracts;
using Simplify.Project.Model;

namespace Simplify.Project.API
{
    internal class AutoMapperConfig : AutoMapper.Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Client, ClientBaseDto>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Name, e => e.MapFrom(x => string.Join(' ', x.Lastname, x.Firstname, x.Patronymic).Trim()));

            CreateMap<Apartment, ApartmentBaseDto>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Number, e => e.MapFrom(x => x.Number));

            CreateMap<Entrance, EntranceBaseDto>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Number, e => e.MapFrom(x => x.Number))
                .ForMember(x => x.Apartments, e => e.MapFrom(x => x.Apartments));

            CreateMap<House, HouseBaseDto>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Number, e => e.MapFrom(x => x.Number))
                .ForMember(x => x.Building, e => e.MapFrom(x => x.Building))
                .ForMember(x => x.Street, e => e.MapFrom(x => x.Street))
                .ForMember(x => x.Entrances, e => e.MapFrom(x => x.Entrances));

            CreateMap<Estate, EstateBaseDto>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Houses, e => e.MapFrom(x => x.Houses))
                .ForMember(x => x.Note, e => e.MapFrom(x => x.Note))
                .ForMember(x => x.Name, e => e.MapFrom(x => x.Name));
        }
    }
}
