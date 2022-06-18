using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Simplify.Project.API.Contracts.Apartment;
using Simplify.Project.API.Contracts.Client;
using Simplify.Project.Front.Helpers;
using Simplify.Project.Shared;

namespace Simplify.Project.Front.Shared.Cards;

public partial class ApartmentEditCard
{
	private readonly Guid _registerAndAddOwnershipButtonId = Guid.NewGuid();
	private readonly Guid _addOwnershipButtonId = Guid.NewGuid();
	private readonly Guid _addOwnershipFamilyButtonId = Guid.NewGuid();
	private readonly Guid _addRenterButtonId = Guid.NewGuid();

	private DetailsCard? _detailsCard;
	private ApartmentEditDto _apartmentEditDto = new();

	private bool _isOpen = false;

	[Inject] private HttpClient? HttpClient { get; set; }
	
	[Inject] private IJSRuntime? JsRuntime { get; set; }

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
	/// <param name="apartmentId">Идентификатор квартиры</param>
	public async Task Init(Guid apartmentId)
	{
		_apartmentEditDto = await GetApartmentEditFromServer(apartmentId);
		StateHasChanged();
	}

	/// <summary>
	/// Открыть карточку
	/// </summary>
	/// <param name="offsetTop">Смещение сверху</param>
	/// <param name="offsetLeft">Смещение слева</param>
	public void Open(double offsetTop, double offsetLeft)
	{
		_detailsCard?.Open(offsetTop, offsetLeft);
		_isOpen = true;
	}

	/// <summary>
	/// Закрыть карточку
	/// </summary>
	public void Close()
	{
		_isOpen = false;
		_addNewClientCard?.Close();
		_addExistClientCard?.Close();
		OnClose?.Invoke();
		_detailsCard?.Close();
	}

	private async Task<ApartmentEditDto> GetApartmentEditFromServer(Guid apartmentId)
	{
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		return await HttpClientHelper.GetJsonFromServer<ApartmentEditDto>(
			HttpClient,
			$"api/apartment/{apartmentId}/for-edit",
			"Произошла ошибка при получении данных квартиры для редактирования!") ?? new ApartmentEditDto();
	}
	
	private async Task ApartmentEditSaveOnClick()
	{
		await SendApartmentEditDataIntoServer(_apartmentEditDto);
		OnClose?.Invoke();
		Close();
	}
	
	private async Task SendApartmentEditDataIntoServer(ApartmentEditDto apartmentEditDto)
	{
		ArgumentNullException.ThrowIfNull(HttpClient, nameof(HttpClient));
		await HttpClientHelper.PatchJsonToServer(
			HttpClient,
			$"api/apartment/{apartmentEditDto.Id}/edit",
			apartmentEditDto,
			$"Произошла ошибка при частичном изменении данных квартиры!");
	}
	
	private void RemoveApartmentRelationWithClient(ApartmentRelationDto relation)
	{
		_apartmentEditDto.ApartmentRelations.Remove(relation);
	}

	private void CreateRelationByClient(ClientBaseDto clientBaseDto, ApartmentRelationType relationType)
	{
		if (_apartmentEditDto.Id == Guid.Empty || clientBaseDto.Id == Guid.Empty)
			return;
		
		var relationDto = new ApartmentRelationDto
		{
			Id = Guid.NewGuid(),
			Apartment = new ApartmentBaseDto
			{
				Id = _apartmentEditDto.Id,
				Number = _apartmentEditDto.Number,
			},
			Client = clientBaseDto,
			Created = DateTime.Now,
			RelationType = relationType,
		};
		
		_apartmentEditDto.ApartmentRelations.Add(relationDto);
		StateHasChanged();
	}

	#region Register and Add Client to Apartment

	private ApartmentAddNewClientCard? _addNewClientCard;
	
	private async Task AddNewClientCardOpen(Guid clickedButtonId)
	{
		ArgumentNullException.ThrowIfNull(JsRuntime, nameof(JsRuntime));
		var coords = await JsRuntime.InvokeAsync<Coordinates>("getElementCoordinatesById", clickedButtonId);
		
		_addNewClientCard?.Open(coords.Y, coords.X);
	}

	#endregion
	
	#region Add Exist Client to Apartment

	private ApartmentAddExistClientCard? _addExistClientCard;
	
	private async Task AddExistClientCardOpen(Guid clickedButtonId, ApartmentRelationType relationType)
	{
		ArgumentNullException.ThrowIfNull(JsRuntime, nameof(JsRuntime));
		var coords = await JsRuntime.InvokeAsync<Coordinates>("getElementCoordinatesById", clickedButtonId);
		
		_addExistClientCard?.Init(_apartmentEditDto, relationType);
		_addExistClientCard?.Open(coords.Y, coords.X);
	}

	#endregion
	
	private class Coordinates
	{
		public double X { get; set; }

		public double Y { get; set; }
	}
}
