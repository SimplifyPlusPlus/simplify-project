using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Simplify.Project.API.Contracts;
using Simplify.Project.API.Contracts.Apartment;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.API.Contracts.Search;
using Simplify.Project.Front.Helpers;
using Simplify.Project.Front.Shared.Cards;
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
		_targetValue = $"{HandbookSearchType.Clients}";

		_onInputDebounced = DebounceEvent<ChangeEventArgs>(
			action: (e => _searchValue = e.Value?.ToString()),
			callback: (async () => await GetSearchResults()),
			interval: TimeSpan.FromMilliseconds(250)
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
		return _targetValue == type.ToString();
	}

	private async Task FiltersItemOnClick(HandbookSearchType type)
	{
		if (FiltersItemIsSelected(type)) return;

		_targetValue = type.ToString();
		await GetSearchResults();
	}

	private async Task GetSearchResults()
	{
		ArgumentNullException.ThrowIfNull(HttpClient);
		SearchResults = string.IsNullOrEmpty(_searchValue) || string.IsNullOrEmpty(_targetValue)
			? new List<SearchResultDto>()
			: await HttpClientHelper.GetJsonFromServer<List<SearchResultDto>>(
				HttpClient,
				$"api/search?searchString={_searchValue}&target={_targetValue}",
				"Произошла ошибка при поиске") ?? new List<SearchResultDto>();
		
		StateHasChanged();
	}

	#region Work with Handbook item's

	private ClientEditCard? _clientEditCard;
	private ApartmentEditCard? _apartmentEditCard;

	private async Task SelectHandbookItem(SearchResultDto searchResultDto)
	{
		ArgumentNullException.ThrowIfNull(JsRuntime, nameof(JsRuntime));
		var coords = await JsRuntime.InvokeAsync<Coordinates>("getElementCoordinatesById", searchResultDto.Id);

		switch (searchResultDto.Type)
		{
			case HandbookSearchType.Clients:
				_clientEditCard?.Init(searchResultDto.Id);
				if (_clientEditCard?.IsOpen == true)
					_clientEditCard?.Close();
				else
					_clientEditCard?.Open(coords.Y, coords.X);
				break;
			case HandbookSearchType.Apartments:
				_apartmentEditCard?.Init(searchResultDto.Id);
				if (_apartmentEditCard?.IsOpen == true)
					_apartmentEditCard.Close();
				else
					_apartmentEditCard?.Open(coords.Y, coords.X);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(searchResultDto.Type));
		}
		
		StateHasChanged();
	}

	#endregion

	private async Task Refresh()
	{
		await GetSearchResults();
	}

	private class Coordinates
	{
		public double X { get; set; }

		public double Y { get; set; }
	}
}
