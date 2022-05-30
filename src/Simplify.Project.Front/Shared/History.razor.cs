using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Events;
using Simplify.Project.Front.Helpers;

namespace Simplify.Project.Front.Shared;

public partial class History
{
	private List<EventInfo> _eventDtos = new();
	private List<EventInfo> _eventInfoForDisplay = new();
	private string _eventDatePattern = "dd MMMM";

	[Inject] private HttpClient? HttpClient { get; set; }

	protected override async Task OnInitializedAsync()
	{
		_eventDtos = (await GetEventsFromServer())
			.Select(MapEventDto)
			.OrderByDescending(ev => ev.Created)
			.ToList();
		if (_eventDtos.First().Created.Year != _eventDtos.Last().Created.Year)
			_eventDatePattern += " yyyyг.";
		_eventInfoForDisplay = _eventDtos;
		await base.OnInitializedAsync();
	}

	private void HistorySearchOnInput(string? pattern)
	{
		_eventInfoForDisplay = _eventDtos
			.Where(ev => $"{ev.Info} {ev.Created.ToLocalTime().ToString(_eventDatePattern)}".ToLower().Trim()
				.Contains(pattern?.ToLower().Trim() ?? string.Empty))
			.ToList();
	}
	
	private async Task<List<EventDto>> GetEventsFromServer()
	{
		ArgumentNullException.ThrowIfNull(HttpClient);
		return await HttpClientHelper.GetJsonFromServer<List<EventDto>>(
			HttpClient,
			$"api/event",
			"Произошла ошибка при получении списка событий") ?? new List<EventDto>();
	}

	private static EventInfo MapEventDto(EventDto eventDto)
	{
		return new EventInfo
		{
			Created = eventDto.Created,
			Info = EventHelper.GetMarkup(eventDto),
		};
	}
	
	private class EventInfo
	{
		public DateTime Created { get; set; }
		
		public MarkupString Info { get; set; }
	}
}
