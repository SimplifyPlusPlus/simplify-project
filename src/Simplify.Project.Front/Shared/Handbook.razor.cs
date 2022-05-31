using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Contracts.Apartment;
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

	protected override async Task OnInitializedAsync()
	{
		_targetValue = $"{HandbookSearchType.Clients}{HandbookSearchType.Apartments}";

		_onInputDebounced = DebounceEvent<ChangeEventArgs>(
			action: (e => _searchValue = e.Value?.ToString()),
			callback: (async () => await GetSearchResults()),
			interval: TimeSpan.FromMilliseconds(500)
		);
		
		_existClientOnInputDebounced = DebounceEvent<ChangeEventArgs>(
			action: (e => _existClientSearchValue = e.Value?.ToString()),
			callback: (async () => await GetExistClientSearchResults()),
			interval: TimeSpan.FromMilliseconds(500)
		);
		
		await base.OnInitializedAsync();
	}

	private Action<T> DebounceEvent<T>(Action<T> action, Action callback, TimeSpan interval)
	{
		return Debouncer.Debounce<T>(arg =>
		{
			InvokeAsync(async () =>
			{
				action(arg);
				callback();
			});
		}, interval);
	}

	private bool FiltersItemIsSelected(HandbookSearchType type)
	{
		return _targetValue?.Contains(type.ToString()) ?? false;
	}

	private async Task FiltersItemOnClick(HandbookSearchType type)
	{
		_targetValue = _targetValue?.Contains(type.ToString()) ?? false
			? _targetValue.Replace(type.ToString(), "").Trim()
			: string.Concat(_targetValue, type.ToString());

		await GetSearchResults();
	}

	private static string GetComfortableTypeName(HandbookSearchType type)
	{
		return type switch
		{
			HandbookSearchType.Apartments => "Квартира",
			HandbookSearchType.Clients => "Клиент",
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Неизвестный тип данных -> {type}")
		};
	}

	private async Task GetSearchResults()
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

	private async Task RemoveApartmentRelationWithClient(Guid relationId)
	{
		await RemoveApartmentRelationWithClientFromServer(relationId);
		
		_apartmentEditDto = await GetApartmentEditFromServer();
		StateHasChanged();
	}
	
	private async Task RemoveApartmentRelationWithClientFromServer(Guid relationId)
	{
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		await HttpClientHelper.PostJsonToServer(
			HttpClient,
			$"api/apartment/remove-relation",
			relationId,
			"Произошла ошибка при удалении отношения с квартирой!");
	}

	#region Add Exist Client to Apartment

	private DetailsCard? _addExistClientCard;
	private Guid _addOwnershipButtonId = Guid.NewGuid();
	private Guid _addOwnershipFamilyButtonId = Guid.NewGuid();
	private Guid _addRenterButtonId = Guid.NewGuid();
	private ApartmentRelationType _existClientRelationType;
	private List<SearchResultDto> ExistClientSearchResults { get; set; } = new();
	private string? _existClientSearchValue = string.Empty;
	private Action<ChangeEventArgs>? _existClientOnInputDebounced;

	private async Task AddExistClientCardOpen(Guid clickedButtonId, ApartmentRelationType relationType)
	{
		ArgumentNullException.ThrowIfNull(JsRuntime, nameof(JsRuntime));
		var coords = await JsRuntime.InvokeAsync<Coordinates>("getElementCoordinatesById", clickedButtonId);
		_existClientRelationType = relationType;
		
		_addExistClientCard?.Open(coords.Y, coords.X);
	}
	
	private async Task SelectExistClient(SearchResultDto searchResultDto)
	{
		if (_selectApartmentId == null || _selectApartmentId == Guid.Empty)
			return;
		
		var relationDto = new ApartmentRelationCreateDto
		{
			ApartmentId = _selectApartmentId.Value,
			ClientId = searchResultDto.Id,
			RelationType = _existClientRelationType,
		};
		
		await SendApartmentRelationCreateIntoServer(relationDto);
		
		_addExistClientCard?.Close();
		_apartmentEditDto = await GetApartmentEditFromServer();
		ExistClientSearchResults.Clear();
		StateHasChanged();
	}
	
	private async Task GetExistClientSearchResults()
	{
		ArgumentNullException.ThrowIfNull(HttpClient);
		ExistClientSearchResults = _searchValue == string.Empty
			? new List<SearchResultDto>()
			: await HttpClientHelper.GetJsonFromServer<List<SearchResultDto>>(
				HttpClient,
				$"search?searchString={_searchValue}&target={HandbookSearchType.Clients}",
				"Произошла ошибка при поиске клиентов") ?? new List<SearchResultDto>();
		if (_apartmentEditDto.ApartmentRelations.Any())
		{
			ExistClientSearchResults.RemoveAll(item => _apartmentEditDto.ApartmentRelations.Any(relation => relation.Client.Id == item.Id));
		}
		StateHasChanged();
	}
	
	#endregion
	
	#region Register and Add Client to Apartment
	
	private DetailsCard? _registerAndAddClientCard;
	private Guid _registerAndAddClientButtonId = Guid.NewGuid();
	private ClientCreateDto _clientCreateDto = new() { Id = Guid.NewGuid() };

	private async Task AddRegisterClientCardOpen()
	{
		ArgumentNullException.ThrowIfNull(JsRuntime, nameof(JsRuntime));
		var coords = await JsRuntime.InvokeAsync<Coordinates>("getElementCoordinatesById", _registerAndAddClientButtonId);
		
		_registerAndAddClientCard?.Open(coords.Y, coords.X);
	}
	
	private async Task ApartmentRelationCreateSave()
	{
		if (_selectApartmentId == null || _selectApartmentId == Guid.Empty)
			return;
		
		await SendClientCreateIntoServer(_clientCreateDto);
		var relationDto = new ApartmentRelationCreateDto
		{
			ApartmentId = _selectApartmentId.Value,
			ClientId = _clientCreateDto.Id,
			RelationType = ApartmentRelationType.Ownership,
		};
		await SendApartmentRelationCreateIntoServer(relationDto);
		
		_registerAndAddClientCard?.Close();
		_apartmentEditDto = await GetApartmentEditFromServer();
		_clientCreateDto = new ClientCreateDto { Id = Guid.NewGuid() };
		StateHasChanged();
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
	
	private async Task SendApartmentRelationCreateIntoServer(ApartmentRelationCreateDto relationCreateDto)
	{
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		await HttpClientHelper.PostJsonToServer(
			HttpClient,
			$"api/apartment/add-relation",
			relationCreateDto,
			"Произошла ошибка при добавлении отношения с квартирой!");
	}

	#endregion
	
	#region Work with Handbook item's

	private DetailsCard? _clientDetailsCard;
	private Guid? _selectedClientId;
	private ClientEditDto _clientEditDto = new();

	private DetailsCard? _apartmentDetailsCard;
	private Guid? _selectApartmentId;
	private ApartmentEditDto _apartmentEditDto = new();

	private async Task SelectHandbookItem(SearchResultDto searchResultDto)
	{
		ArgumentNullException.ThrowIfNull(JsRuntime, nameof(JsRuntime));
		var coords = await JsRuntime.InvokeAsync<Coordinates>("getElementCoordinatesById", searchResultDto.Id);

		switch (searchResultDto.Type)
		{
			case HandbookSearchType.Clients:
				_selectedClientId = searchResultDto.Id;
				_clientEditDto = await GetClientEditFromServer();
				_clientDetailsCard?.Open(coords.Y, coords.X);
				break;
			case HandbookSearchType.Apartments:
				_selectApartmentId = searchResultDto.Id;
				_apartmentEditDto = await GetApartmentEditFromServer();
				_apartmentDetailsCard?.Open(coords.Y, coords.X);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(searchResultDto.Type));
		}
		
		StateHasChanged();
	}

	private async Task ClientEditSaveOnClick()
	{
		await SendClientEditDataIntoServer(_clientEditDto);
		await GetSearchResults();
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
	
	private async Task ApartmentEditSaveOnClick()
	{
		await SendApartmentEditDataIntoServer(_apartmentEditDto);
		await GetSearchResults();
		StateHasChanged();
	}
	
	private async Task<ApartmentEditDto> GetApartmentEditFromServer()
	{
		ArgumentNullException.ThrowIfNull(_selectApartmentId, nameof(_selectApartmentId));
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		var k = await HttpClientHelper.GetJsonFromServer<ApartmentEditDto>(
			HttpClient,
			$"api/apartment/{_selectApartmentId}/for-edit",
			"Произошла ошибка при получении данных квартиры для редактирования!") ?? new ApartmentEditDto();
		return k;
	}
	
	private async Task SendApartmentEditDataIntoServer(ApartmentEditDto apartmentEditDto)
	{
		ArgumentNullException.ThrowIfNull(_selectApartmentId, nameof(_selectApartmentId));
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		await HttpClientHelper.PatchJsonToServer(
			HttpClient,
			$"api/apartment/{_selectApartmentId}/edit",
			apartmentEditDto,
			$"Произошла ошибка при частичном изменении данных квартиры!");
	}

	#endregion

	private class Coordinates
	{
		public double X { get; set; }

		public double Y { get; set; }
	}
}
