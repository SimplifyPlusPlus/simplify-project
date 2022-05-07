using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Employee;
using Simplify.Project.Front.Helpers;
using Simplify.Project.Shared;

namespace Simplify.Project.Front.Shared;

public partial class EmployeeCreateCard
{
	[Inject] 
	private HttpClient? HttpClient { get; set; }
	
	private EmployeeCreateDto _employeeCreateDto = new();
	private string _roleMouseUp = string.Empty;
	private bool _showCreateForm = false;

	public void ResetView()
	{
		_showCreateForm = false;
		_employeeCreateDto = new EmployeeCreateDto();
		_roleMouseUp = string.Empty;
		StateHasChanged();
	}
	
	private async Task SendEmployeesCreateDataToServer(EmployeeCreateDto employeeCreateDto)
	{
		ArgumentNullException.ThrowIfNull(HttpClient);
		await HttpClientHelper.PostJsonToServer(
			HttpClient,
			"api/employee/add",
			employeeCreateDto,
			$"Произошла ошибка при добавлении нового сотрудника!");
	}
	
	private void NewEmployeeRolePicked(string role)
	{
		_showCreateForm = true;
		_employeeCreateDto = new EmployeeCreateDto { Role = role };
		_roleMouseUp = string.Empty;
		StateHasChanged();
	}

	private static string GetRoleDescription(string role)
	{
		return role switch
		{
			RoleType.Administrator => "",
			RoleType.Manager => "",
			RoleType.Editor => "",
			RoleType.Dispatcher => "Диспетчер имеет доступ к заявкам пользователей и может отвечать на них. Также диспетчер может посылать уведомления.",
			_ => string.Empty,
		};
	}
}
