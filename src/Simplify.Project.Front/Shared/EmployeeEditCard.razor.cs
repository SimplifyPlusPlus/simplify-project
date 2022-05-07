using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Employee;

namespace Simplify.Project.Front.Shared;

public partial class EmployeeEditCard
{
	[Parameter]
	public Guid EmployeeId { get; set; }

	private EmployeeEditDto _employeeEditDto = new();
}
