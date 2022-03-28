namespace Simplify.Project.Model.Handbooks;

/// <summary>
/// Тип связи клиента с квартирой
/// </summary>
public static class ApartmentRelationType
{
	/// <summary>
	/// Не указано
	/// </summary>
	public const string Empty = "Не указано";
	
	/// <summary>
	/// Собственник
	/// </summary>
	public const string Ownership = "Собственник";
	
	/// <summary>
	/// Родственник собственника
	/// </summary>
	public const string OwnershipFamily = "Родственник собственника";
	
	/// <summary>
	/// Арендатор
	/// </summary>
	public const string Renter = "Арендатор";
	
	/// <summary>
	/// Получить связи в виде списка
	/// </summary>
	public static List<string> ToList() => new()
	{
		Empty, Ownership, OwnershipFamily, Renter
	};
}