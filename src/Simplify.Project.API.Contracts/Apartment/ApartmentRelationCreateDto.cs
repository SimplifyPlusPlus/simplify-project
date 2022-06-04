using Simplify.Project.Shared;

namespace Simplify.Project.API.Contracts.Apartment;

public class ApartmentRelationCreateDto
{
	public Guid ApartmentId { get; set; }

	public ApartmentRelationType RelationType { get; set; }

	public Guid ClientId { get; set; }
}
