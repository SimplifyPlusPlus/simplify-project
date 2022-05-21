using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.API.Contracts.Search;
using Simplify.Project.Front.Helpers;
using Simplify.Project.Shared;

namespace Simplify.Project.Front.Shared;

public partial class Handbook
{
	private string? _searchValue = string.Empty;
	private string? _targetValue = string.Empty;
	private Action<ChangeEventArgs>? _onInputDebounced;
	private List<SearchResultDto> SearchResults { get; set; } = new();

	[Inject] private HttpClient? HttpClient { get; set; }

	[Inject] private IJSRuntime? JsRuntime { get; set; }

	protected override void OnInitialized()
	{
		_targetValue = $"{HandbookSearchTypes.Clients} {HandbookSearchTypes.Apartments} {HandbookSearchTypes.Houses}";
		_onInputDebounced = DebounceEvent<ChangeEventArgs>(e => _searchValue = e.Value?.ToString(), TimeSpan.FromMilliseconds(500));

		_searchValue = "м";
		GetSearchResults();

		base.OnInitialized();
	}

	private Action<T> DebounceEvent<T>(Action<T> action, TimeSpan interval)
	{
		return Debouncer.Debounce<T>(arg =>
		{
			InvokeAsync(() =>
			{
				action(arg);
				GetSearchResults();
			});
		}, interval);
	}

	private bool FiltersItemIsSelected(HandbookSearchTypes type)
	{
		return _targetValue?.Contains(type.ToString()) ?? false;
	}

	private void FiltersItemOnClick(HandbookSearchTypes type)
	{
		_targetValue = _targetValue?.Contains(type.ToString()) ?? false
			? _targetValue.Replace(type.ToString(), "").Trim()
			: string.Concat(_targetValue, type.ToString());

		GetSearchResults();
	}

	private static string GetComfortableTypeName(HandbookSearchTypes type)
	{
		return type switch
		{
			HandbookSearchTypes.Houses => "Дом",
			HandbookSearchTypes.Apartments => "Квартира",
			HandbookSearchTypes.Clients => "Клиент",
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Неизвестный тип данных -> {type}")
		};
	}

	private async void GetSearchResults()
	{
		ArgumentNullException.ThrowIfNull(HttpClient);
		SearchResults = _searchValue == string.Empty || _targetValue == string.Empty
			? new List<SearchResultDto>()
			: await HttpClientHelper.GetJsonFromServer<List<SearchResultDto>>(
				HttpClient,
				$"search?searchString={_searchValue}&target={_targetValue}",
				"Произошла ошибка при поиске") ?? new List<SearchResultDto>();
		StateHasChanged();
	}

	#region Work with Handbook item's

	private DetailsCard? _clientDetailsCard;
	private Guid? _selectedClientId;
	private ClientEditDto _clientEditDto = new();

	private async Task SelectHandbookItem(SearchResultDto searchResultDto)
	{
		Console.WriteLine($"Click {searchResultDto.Name}");
		ArgumentNullException.ThrowIfNull(JsRuntime, nameof(JsRuntime));
		var coords = await JsRuntime.InvokeAsync<Coordinates>("getElementCoordinatesById", searchResultDto.Id);

		switch (searchResultDto.Type)
		{
			case HandbookSearchTypes.Clients:
				_selectedClientId = searchResultDto.Id;
				_clientEditDto = await GetClientEditFromServer();
				_clientDetailsCard?.Open(coords.Y);
				// if (_clientDetailsCard != null)
				// {
				// 	_clientDetailsCard.Open(coords.Y);
				// 	_clientDetailsCard.OnClose = () =>
				// 	{
				// 		_selectedClientId = null;
				// 		_clientEditDto = new ClientEditDto();
				// 		StateHasChanged();
				// 	};
				// }
				break;
			case HandbookSearchTypes.Apartments:
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(searchResultDto.Type));
		}
		
		StateHasChanged();
	}

	private void Clear()
	{
		
	}

	private async Task ClientEditSaveOnClick()
	{
		await SendClientEditDataIntoServer(_clientEditDto);
		GetSearchResults();
		StateHasChanged();
	}
	
	private async Task<ClientEditDto> GetClientEditFromServer()
	{
		ArgumentNullException.ThrowIfNull(_selectedClientId, nameof(_selectedClientId));
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		return await HttpClientHelper.GetJsonFromServer<ClientEditDto>(
			HttpClient,
			$"api/client/{_selectedClientId}/for-edit",
			"Произошла ошибка при получении данных клиента для редактирования") ?? new ClientEditDto();
	}
	
	private async Task SendClientEditDataIntoServer(ClientEditDto clientEditDto)
	{
		ArgumentNullException.ThrowIfNull(_selectedClientId);
		ArgumentNullException.ThrowIfNull(HttpClient);
		await HttpClientHelper.PatchJsonToServer(
			HttpClient,
			$"api/client/{_selectedClientId}/edit",
			clientEditDto,
			$"Произошла ошибка при частичном изменении данных клиента!");
	}

	#endregion

	private class Coordinates
	{
		public int X { get; set; }

		public int Y { get; set; }
	}

	private class Dimension
	{
		public int Width { get; set; }

		public int Height { get; set; }
	}
}
