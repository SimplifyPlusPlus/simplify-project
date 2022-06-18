using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Employee;
using Simplify.Project.Front.Helpers;

namespace Simplify.Project.Front.Shared.Cards;

public partial class EmployeeEditCard
{
	[Inject] 
	private HttpClient? HttpClient { get; set; }
	
	[Parameter]
	public Guid? EmployeeId { get; set; }

	[CascadingParameter]
	public Employees? EmployeesBase { get; set; }
	
	private EmployeeEditDto _employeeEditDto = new();

	protected override async Task OnParametersSetAsync()
	{
		_employeeEditDto = await GetEmployeeForEditDataFromServer();
		await base.OnParametersSetAsync();
	}

	/// <summary>
	/// Сброс представления
	/// </summary>
	public void ResetView()
	{
		_employeeEditDto = new EmployeeEditDto();
		EmployeeId = null;
		StateHasChanged();
	}
	
	private async Task<EmployeeEditDto> GetEmployeeForEditDataFromServer()
	{
		ArgumentNullException.ThrowIfNull(EmployeeId);
		ArgumentNullException.ThrowIfNull(HttpClient);
		return await HttpClientHelper.GetJsonFromServer<EmployeeEditDto>(
			HttpClient,
			$"api/employee/{EmployeeId}/for-edit",
			$"Произошла ошибка при получении данных сотрудника для изменения") ?? new EmployeeEditDto();
	}
	
	private async Task SendEmployeesEditDataToServer(EmployeeEditDto employeeEditDto)
	{
		ArgumentNullException.ThrowIfNull(EmployeeId);
		ArgumentNullException.ThrowIfNull(HttpClient);
		await HttpClientHelper.PatchJsonToServer(
			HttpClient,
			$"api/employee/{EmployeeId}/edit",
			employeeEditDto,
			$"Произошла ошибка при частичном изменении данных сотрудника!");
	}
	
	private async Task EmployeeEditOnClick()
	{
		await SendEmployeesEditDataToServer(_employeeEditDto);
		ResetView();
		if (EmployeesBase != null)
			await EmployeesBase.ResetView();
	}
}
