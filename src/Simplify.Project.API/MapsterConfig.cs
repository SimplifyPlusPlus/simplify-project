using Mapster;
using Simplify.Project.Model;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Contracts.Employee;
using Simplify.Project.Shared;

namespace Simplify.Project.API;

internal static class MapsterConfig
{
	public static void Config()
	{
		TypeAdapterConfig<Client, ClientBaseDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Name, src => $"{src.Lastname} {src.Firstname} {src.Patronymic}".Trim());

		TypeAdapterConfig<Client, ClientDetailedDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Name, src => $"{src.Lastname} {src.Firstname} {src.Patronymic}".Trim())
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.Phone, src => src.Phone)
			.Map(dest => dest.IsBlocked, src => src.IsBlocked)
			.Map(dest => dest.Note, src => src.Note);

		TypeAdapterConfig<Employee, EmployeeBaseDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Name, src => $"{src.Lastname} {src.Firstname} {src.Patronymic}".Trim())
			.Map(dest => dest.Role, src => src.Role)
			.Map(dest => dest.IsBlocked, src => src.IsBlocked);

		TypeAdapterConfig<Employee, EmployeeDetailedDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Name, src => $"{src.Lastname} {src.Firstname} {src.Patronymic}".Trim())
			.Map(dest => dest.Role, src => src.Role)
			.Map(dest => dest.Login, src => src.Login)
			.Map(dest => dest.IsBlocked, src => src.IsBlocked)
			.Map(dest => dest.Note, src => src.Note);
		
		TypeAdapterConfig<EmployeeCreateDto, Employee>.NewConfig()
			.Map(dest => dest.Id, src => Guid.NewGuid())
			.Map(dest => dest.Lastname, src => src.Lastname)
			.Map(dest => dest.Firstname, src => src.Firstname)
			.Map(dest => dest.Patronymic, src => src.Patronymic)
			.Map(dest => dest.Created, src => DateTime.Now)
			.Map(dest => dest.Role, src => src.Role)
			.Map(dest => dest.Login, src => src.Login)
			.Map(dest => dest.Password, src => src.Password)
			.Map(dest => dest.Note, src => src.Note)
			.Map(dest => dest.IsBlocked, src => false);
		
		TypeAdapterConfig<Employee, EmployeeEditDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Lastname, src => src.Lastname)
			.Map(dest => dest.Firstname, src => src.Firstname)
			.Map(dest => dest.Patronymic, src => src.Patronymic)
			.Map(dest => dest.Role, src => src.Role)
			.Map(dest => dest.Login, src => src.Login)
			.Map(dest => dest.Password, src => src.Password)
			.Map(dest => dest.Note, src => src.Note)
			.Map(dest => dest.IsBlocked, src => src.IsBlocked);
		
		TypeAdapterConfig<EmployeeEditDto, Employee>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Lastname, src => src.Lastname)
			.Map(dest => dest.Firstname, src => src.Firstname)
			.Map(dest => dest.Patronymic, src => src.Patronymic)
			.Map(dest => dest.Role, src => src.Role)
			.Map(dest => dest.Login, src => src.Login)
			.Map(dest => dest.Password, src => src.Password)
			.Map(dest => dest.Note, src => src.Note)
			.Map(dest => dest.IsBlocked, src => src.IsBlocked);

		TypeAdapterConfig<Apartment, ApartmentBaseDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Number, src => src.Number);

		TypeAdapterConfig<Entrance, EntranceBaseDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Number, src => src.Number)
			.Map(dest => dest.Apartments, src => src.Apartments);

		TypeAdapterConfig<House, HouseBaseDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Location, src => $"{src.Street} {src.Number} {src.Building}".Trim())
			.Map(dest => dest.Entrances, src => src.Entrances);

		TypeAdapterConfig<Estate, EstateBaseDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Name, src => src.Name)
			.Map(dest => dest.Note, src => src.Note)
			.Map(dest => dest.Houses, src => src.Houses);

		TypeAdapterConfig<ApartmentRelation, ApartmentRelationDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Client, src => src.Client)
			.Map(dest => dest.Created, src => src.Created)
			.Map(dest => dest.Apartment, src => src.Apartment)
			.Map(dest => dest.RelationType, src => src.RelationType);

		TypeAdapterConfig<House, SearchResultDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Name, src => $"{src.Street} {src.Number} {src.Building}".Trim())
			.Map(dest => dest.Type, src => HandbookSearchTypes.Houses);

		TypeAdapterConfig<Client, SearchResultDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Name, src => $"{src.Lastname} {src.Firstname} {src.Patronymic}".Trim())
			.Map(dest => dest.Type, src => HandbookSearchTypes.Clients);

		TypeAdapterConfig<Apartment, SearchResultDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Name, src => $"{src.Entrance.House.Street} {src.Entrance.House.Number} {src.Entrance.House.Building}".Trim() + $"{src.Number}")
			.Map(dest => dest.Type, src => HandbookSearchTypes.Apartments);
	}
}
