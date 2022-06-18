namespace Simplify.Project.Shared;

/// <summary>
/// Тип связи клиента с квартирой
/// </summary>
public enum ApartmentRelationType
{
	/// <summary>
	/// Собственник
	/// </summary>
	Ownership = 1,
	
	/// <summary>
	/// Родственник собственника
	/// </summary>
	OwnershipFamily = 2,
	
	/// <summary>
	/// Арендатор
	/// </summary>
	Renter = 3,
}
