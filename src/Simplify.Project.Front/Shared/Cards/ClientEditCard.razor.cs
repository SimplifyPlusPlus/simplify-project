using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.Front.Helpers;

namespace Simplify.Project.Front.Shared.Cards;

public partial class ClientEditCard
{
	private DetailsCard? _detailsCard;
	private Guid? _selectedClientId;
	private ClientEditDto _clientEditDto = new();
	private bool _isOpen = false;

	[Inject] private HttpClient? HttpClient { get; set; }
	
	/// <summary>
	/// Коллбек при закрытие карточки
	/// </summary>
	[Parameter]
	public Action? OnClose { get; set; }

	/// <summary>
	/// Признак того, что карточка открыта
	/// </summary>
	/// <returns>True, если карточка открыта, иначе False</returns>
	public bool IsOpen() => _isOpen;
	
	/// <summary>
	/// Инициализировать карточку
	/// </summary>
	/// <param name="clientId">Идентификатор клиента</param>
	public async void Init(Guid clientId)
	{
		_clientEditDto = new ClientEditDto();
		_selectedClientId = clientId;
		_clientEditDto = await GetClientEditFromServer();
		StateHasChanged();
	}

	/// <summary>
	/// Открыть карточку
	/// </summary>
	/// <param name="offsetTop">Смещение сверху</param>
	/// <param name="offsetLeft">Смещение слева</param>
	public void Open(double offsetTop, double offsetLeft)
	{
		_isOpen = true;
		_detailsCard?.Open(offsetTop, offsetLeft);
	}
	
	/// <summary>
	/// Закрыть карточку
	/// </summary>
	public void Close()
	{
		_isOpen = false;
		_detailsCard?.Close();
		OnClose?.Invoke();
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
