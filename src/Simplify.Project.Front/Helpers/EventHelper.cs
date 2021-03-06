using Microsoft.AspNetCore.Components;
using Simplify.Project.API.Contracts.Events;
using Simplify.Project.Shared;

namespace Simplify.Project.Front.Helpers;

/// <summary>
/// Помощник формирования событий
/// </summary>
public static class EventHelper
{
	/// <summary>
	/// Формирование события из данных
	/// </summary>
	/// <param name="eventDto">Данные события</param>
	/// <returns>Markup-строка с информацией о событии</returns>
	/// <exception cref="ArgumentOutOfRangeException">Неизвестрный тип сущности</exception>
	public static MarkupString GetMarkup(EventDto eventDto)
	{
		var markup = "<div>";

		if (eventDto.Data.ContainsKey(EventDataKeys.Creator))
		{
			markup += "<span>Сотрудник </span>";
			markup += $"<a class='events-entity-title'>{eventDto.Data[EventDataKeys.Creator]}</a>";
		}

		markup += eventDto.EventEntityType switch
		{
			EventEntityType.Employee => string.Empty,
			EventEntityType.Client => ConfigureClient(eventDto.EventType, eventDto.Data),
			EventEntityType.Apartment => string.Empty,
			EventEntityType.ApartmentRelation => ConfigureApartmentRelation(eventDto.EventType, eventDto.Data),
			_ => throw new ArgumentOutOfRangeException()
		};

		markup += "</div>";
		return (MarkupString) markup;
	}

	private static string ConfigureClient(EventType eventType, IDictionary<string, object> data)
	{
		var action = eventType switch
		{
			EventType.Create => "зарегистрировал",
			EventType.Edit => "отредактировать",
			EventType.Remove => "заблокировал",
			_ => throw new ArgumentOutOfRangeException()
		};
			
		var client = $"<a class='events-entity-title'>{data[EventDataKeys.Client]}</a>";
		return $"<span> {action} жителя </span> {client}";
	}
	
	private static string ConfigureApartmentRelation(EventType eventType, IDictionary<string, object> data)
	{
		var action = eventType switch
		{
			EventType.Create => "назначил",
			EventType.Edit => string.Empty,
			EventType.Remove => "убрал",
			_ => throw new ArgumentOutOfRangeException()
		};
		
		var relationType = (ApartmentRelationType) Convert.ToInt32(data[EventDataKeys.RelationType].ToString());
		var relation = relationType switch
		{
			ApartmentRelationType.Ownership => eventType == EventType.Remove ? "из владельцев" : "владельцем",
			ApartmentRelationType.OwnershipFamily => eventType == EventType.Remove ? "из доверенных лиц" : "доверенным лицом",
			ApartmentRelationType.Renter => eventType == EventType.Remove ? "из квартирантов" : "квартирантом",
			_ => throw new ArgumentOutOfRangeException()
		};

		var client = $"<a class='events-entity-title'>{data[EventDataKeys.Client]}</a>";
		var apartment = $"<a class='events-entity-title'>{data[EventDataKeys.Apartment]}</a>";
		return $"<span> {action} жителя </span> {client} <span> {relation} </span> {apartment}";
	}
}
