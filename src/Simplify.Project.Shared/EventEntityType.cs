namespace Simplify.Project.Shared;

/// <summary>
/// Типы сущностей, с которыми происходят события
/// </summary>
public enum EventEntityType
{
	/// <summary>
	/// Сотрудник
	/// </summary>
	Employee = 1,
	
	/// <summary>
	/// Житель
	/// </summary>
	Client = 2,
	
	/// <summary>
	/// Квартира
	/// </summary>
	Apartment = 3,
	
	/// <summary>
	/// Отношение к квартире
	/// </summary>
	ApartmentRelation = 4,
}
