namespace Simplify.Project.API.Contracts.Apartment;

public class ApartmentRelationRemoveDto
{
	public Guid ApartmentId { get; set; }

	public Guid ClientId { get; set; }
}
