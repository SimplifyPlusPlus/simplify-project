using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts;
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

	protected override void OnInitialized()
	{
		_targetValue = $"{HandbookSearchTypes.Clients} {HandbookSearchTypes.Apartments} {HandbookSearchTypes.Houses}";
		_onInputDebounced = DebounceEvent<ChangeEventArgs>(e => _searchValue = e.Value?.ToString(), TimeSpan.FromMilliseconds(500));
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
		SearchResults = _searchValue == string.Empty 
			? new List<SearchResultDto>() 
			: await HttpClientHelper.GetJsonFromServer<List<SearchResultDto>>(
				HttpClient,
				$"search?searchString={_searchValue}&target={_targetValue}",
				"Произошла ошибка при поиске") ?? new List<SearchResultDto>();
		StateHasChanged();
	}
}
