using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Contracts.Employee;
using Simplify.Project.Front.Helpers;
using Simplify.Project.Shared;

namespace Simplify.Project.Front.Shared;

public partial class Employees
{
	[Inject] 
	private HttpClient? HttpClient { get; set; }

	private List<EmployeeBaseDto> _employees = new();
	private EmployeeCreateCard? _employeeCreateCard;
	private EmployeeEditCard? _employeeEditCard;

	private Guid _selectedEmployeeId = Guid.Empty;
	private string _filterPattern = string.Empty;

	/// <summary>
	/// Сброс представления
	/// </summary>
	public async Task ResetView()
	{
		_employees = await GetEmployeesFromServer();
		_selectedEmployeeId = Guid.Empty;
		StateHasChanged();
	}
	
	protected override async Task OnInitializedAsync()
	{
		_employees = await GetEmployeesFromServer();
		await base.OnInitializedAsync();
	}

	private async Task<List<EmployeeBaseDto>> GetEmployeesFromServer()
	{
		ArgumentNullException.ThrowIfNull(HttpClient);
		return await HttpClientHelper.GetJsonFromServer<List<EmployeeBaseDto>>(
			HttpClient,
			"api/employee/base-information",
			"Произошла ошибка при получении данных о сотрудниках") ?? new List<EmployeeBaseDto>();
	}

	private async Task<EmployeeDetailedDto> GetEmployeesDetailedInfoFromServer(EmployeeBaseDto employee)
	{
		ArgumentNullException.ThrowIfNull(HttpClient);
		return await HttpClientHelper.GetJsonFromServer<EmployeeDetailedDto>(
	       HttpClient,
	       $"api/employee/{employee.Id}/detailed",
	       $"Произошла ошибка при получении детальной информации о сотруднике {employee.Name}") 
		       ?? new EmployeeDetailedDto();
	}

	private void SelectEmployee(Guid employeeId)
	{
		_employeeCreateCard?.ResetView();
		_employeeEditCard?.ResetView();
		_selectedEmployeeId = employeeId;
		StateHasChanged();
	}
}
