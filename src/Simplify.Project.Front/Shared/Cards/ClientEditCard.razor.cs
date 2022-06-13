using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.Front.Helpers;

namespace Simplify.Project.Front.Shared.Cards;

public partial class ClientEditCard
{
	private DetailsCard? _detailsCard;
	private Guid? _selectedClientId;
	private ClientEditDto _clientEditDto = new();

	[Inject] private HttpClient? HttpClient { get; set; }
	
	[Parameter]
	public Action? OnClose { get; set; }

	public async void Init(Guid clientId)
	{
		_clientEditDto = new ClientEditDto();
		_selectedClientId = clientId;
		_clientEditDto = await GetClientEditFromServer();
		StateHasChanged();
	}
	
	public void Open(double offsetTop, double offsetLeft)
	{
		_detailsCard?.Open(offsetTop, offsetLeft);
	}
	
	private async Task ClientEditSaveOnClick()
	{
		await SendClientEditDataIntoServer(_clientEditDto);
		_detailsCard?.Close();
		
		OnClose?.Invoke();
		StateHasChanged();
	}
	
	private async Task<ClientEditDto> GetClientEditFromServer()
	{
		ArgumentNullException.ThrowIfNull(_selectedClientId, nameof(_selectedClientId));
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		
		return await HttpClientHelper.GetJsonFromServer<ClientEditDto>(
			HttpClient,
			$"api/client/{_selectedClientId}/for-edit",
			"Произошла ошибка при получении данных клиента для редактирования!") ?? new ClientEditDto();
	}
	
	private async Task SendClientEditDataIntoServer(ClientEditDto clientEditDto)
	{
		ArgumentNullException.ThrowIfNull(_selectedClientId, nameof(_selectedClientId));
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		
		await HttpClientHelper.PatchJsonToServer(
			HttpClient,
			$"api/client/{_selectedClientId}/edit",
			clientEditDto,
			$"Произошла ошибка при частичном изменении данных клиента!");
	}
}
