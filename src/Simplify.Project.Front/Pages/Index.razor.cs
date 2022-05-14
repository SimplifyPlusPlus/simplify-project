namespace Simplify.Project.Front.Pages;

public partial class Index
{
	private string _selectedNavItem = NavItem.Handbook;

	private static class NavItem
	{
		public static string History => "История";

		public static string Employees => "Сотрудники";
		
		public static string Handbook => "Справочник";

		public static IEnumerable<string> ToEnumerable() => new[]
		{
			History, Employees, Handbook
		};
	}
}
