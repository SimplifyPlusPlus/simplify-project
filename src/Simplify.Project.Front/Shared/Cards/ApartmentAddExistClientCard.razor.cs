using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Apartment;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.API.Contracts.Search;
using Simplify.Project.Front.Helpers;
using Simplify.Project.Shared;

namespace Simplify.Project.Front.Shared.Cards;

public partial class ApartmentAddExistClientCard
{
	private DetailsCard? _detailsCard;
	private Action<ChangeEventArgs>? _onInputDebounced;

	private List<SearchClientResultDto> _searchResults = new();
	private string? _searchValue = string.Empty;
	
	private ApartmentEditDto _apartmentEditDto = new();
	private ApartmentRelationType _relationType;
	
	[Inject] private HttpClient? HttpClient { get; set; }
	
	[Parameter]
	public Action<ApartmentEditDto>? OnClose { get; set; }

	protected override async Task OnInitializedAsync()
	{
		_onInputDebounced = DebounceEvent<ChangeEventArgs>(
			action: (e => _searchValue = e.Value?.ToString()),
			callback: (async () => await GetExistClientSearchResults()),
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
	
	public void Init(ApartmentEditDto apartmentEditDto, ApartmentRelationType relationType)
	{
		_apartmentEditDto = apartmentEditDto;
		_relationType = relationType;
		_searchResults.Clear();
		_searchValue = string.Empty;
		StateHasChanged();
	}
	
	public void Open(double offsetTop, double offsetLeft)
	{
		_detailsCard?.Open(offsetTop, offsetLeft);
	}

	private void Close()
	{
		_detailsCard?.Close();
		OnClose?.Invoke(_apartmentEditDto);
	}

	private void SelectExistClient(SearchClientResultDto resultDto)
	{
		if (_apartmentEditDto.Id == Guid.Empty)
			return;
		
		var relationDto = new ApartmentRelationDto
		{
			Id = Guid.NewGuid(),
			Apartment = new ApartmentBaseDto
			{
				Id = _apartmentEditDto.Id,
				Number = _apartmentEditDto.Number,
			},
			Client = new ClientBaseDto
			{
				Id = resultDto.Id,
				Name = resultDto.Name,
				IsBlocked = resultDto.IsBlocked,
			},
			Created = DateTime.Now,
			RelationType = _relationType,
		};
		
		_apartmentEditDto.ApartmentRelations.Add(relationDto);
		Close();
	}
	
	private async Task GetExistClientSearchResults()
	{
		ArgumentNullException.ThrowIfNull(HttpClient);
		var result = string.IsNullOrEmpty(_searchValue)
			? new List<SearchClientResultDto>()
			: await HttpClientHelper.GetJsonFromServer<List<SearchClientResultDto>>(
				HttpClient,
				$"api/search/client?searchString={_searchValue}",
				"Произошла ошибка при поиске клиентов") ?? new List<SearchClientResultDto>();
		
		if (_apartmentEditDto.ApartmentRelations.Any())
		{
			result.RemoveAll(item => _apartmentEditDto.ApartmentRelations.Any(relation => relation.Client.Id == item.Id));
		}

		_searchResults = result;
		StateHasChanged();
	}
}
