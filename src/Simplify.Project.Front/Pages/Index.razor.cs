using Microsoft.AspNetCore.Components;

namespace Simplify.Project.Front.Pages;

public partial class Index
{
	private string _selectedNavItem = NavItem.Employees;

	private static class NavItem
	{
		public static string History { get; set; } = "История";
		
		public static string Employees { get; set; } = "Сотрудники";
		
		public static string Handbook { get; set; } = "Справочник";

		public static IEnumerable<string> ToEnumerable() => new[]
		{
			History, Employees, Handbook
		};
	}
}
