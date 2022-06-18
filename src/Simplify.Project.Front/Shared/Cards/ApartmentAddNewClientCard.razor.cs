using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.Front.Helpers;
using Simplify.Project.Shared;

namespace Simplify.Project.Front.Shared.Cards;

public partial class ApartmentAddNewClientCard
{
	private DetailsCard? _detailsCard;
	private readonly ClientCreateDto _clientCreateDto = new() { Id = Guid.NewGuid() };

	[Inject] private HttpClient? HttpClient { get; set; }

	/// <summary>
	/// Коллбек на создание связи
	/// </summary>
	[Parameter, EditorRequired]
	public Action<ClientBaseDto, ApartmentRelationType> OnCreateCallback { get; set; } = null!;
	
	/// <summary>
	/// Открыть карточку
	/// </summary>
	/// <param name="offsetTop">Смещение сверху</param>
	/// <param name="offsetLeft">Смещение слева</param>
	public void Open(double offsetTop, double offsetLeft)
	{
		_detailsCard?.Open(offsetTop, offsetLeft);
	}
	
	/// <summary>
	/// Закрыть карточку
	/// </summary>
	public void Close()
	{
		_detailsCard?.Close();
	}

	private async Task CreateClient()
	{
		await SendClientCreateIntoServer(_clientCreateDto);
		
		var clientBaseDto = new ClientBaseDto
		{
			Id = _clientCreateDto.Id,
			Name = $"{_clientCreateDto.Lastname} {_clientCreateDto.Firstname} {_clientCreateDto.Patronymic}".Trim(),
			IsBlocked = false,
		};
		
		OnCreateCallback.Invoke(clientBaseDto, ApartmentRelationType.Ownership);
		Close();
	}
	
	private async Task SendClientCreateIntoServer(ClientCreateDto clientCreateDto)
	{
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		await HttpClientHelper.PostJsonToServer(
			HttpClient,
			$"api/client/add",
			clientCreateDto,
			"Произошла ошибка при добавлении клиента!");
	}
}
