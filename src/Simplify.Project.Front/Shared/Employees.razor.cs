using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts;
using Simplify.Project.Front.Helpers;
using Simplify.Project.Shared;

namespace Simplify.Project.Front.Shared;

public partial class Employees
{
	private List<EmployeeBaseDto> _employees = new();
	private EmployeeDetailedDto? _employeeDetailedDto;
	private string _roleMouseUp = string.Empty;
	private string _filterPattern = string.Empty;

	[Inject] 
	private HttpClient HttpClient { get; set; }

	protected override async Task OnInitializedAsync()
	{
		_employees = await GetEmployeesFromServer();

		await base.OnInitializedAsync();
	}

	private async Task<List<EmployeeBaseDto>> GetEmployeesFromServer()
	{
		return await HttpClientHelper.GetJsonFromServer<List<EmployeeBaseDto>>(
			HttpClient,
			"api/employee/base-information",
			"Произошла ошибка при получении данных о сотрудниках") ?? new List<EmployeeBaseDto>();
	}

	private async Task<EmployeeDetailedDto> GetEmployeesDetailedInfoFromServer(EmployeeBaseDto employee)
	{
		return await HttpClientHelper.GetJsonFromServer<EmployeeDetailedDto>(
			       HttpClient,
			       $"api/employee/{employee.Id}/detailed",
			       $"Произошла ошибка при получении детальной информации о сотруднике {employee.Name}") ??
		       new EmployeeDetailedDto();
	}

	private void EmployeeSaveOnClick()
	{
		if (_employeeDetailedDto?.Id != Guid.Empty)
		{
			return;
		}
	}
	
	private async Task SelectEmployee(EmployeeBaseDto employee)
	{
		_employeeDetailedDto = await GetEmployeesDetailedInfoFromServer(employee);
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
