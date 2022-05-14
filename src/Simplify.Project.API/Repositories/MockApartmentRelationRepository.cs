using Simplify.Project.Model;
using Simplify.Project.Shared;

namespace Simplify.Project.API.Repositories;

/// <summary>
/// Репозиторий отношений с квартирами
/// </summary>
public class MockApartmentRelationRepository : IApartmentRelationRepository
{
	private readonly List<ApartmentRelation> _apartmentRelations;

	/// <summary>
	/// Конструктор класса <see cref="MockApartmentRelationRepository"/>
	/// </summary>
	public MockApartmentRelationRepository()
	{
		_apartmentRelations = GenerateData();
	}
	
	/// <inheritdoc cref="IApartmentRelationRepository.GetRelations()"/>
	public IQueryable<ApartmentRelation> GetRelations()
	{
		return _apartmentRelations.AsQueryable();
	}

	/// <inheritdoc cref="IApartmentRelationRepository.GetRelation(Guid)"/>
	public ApartmentRelation? GetRelation(Guid id)
	{
		var relation = _apartmentRelations.SingleOrDefault(relation => relation.Id == id);
		return relation;
	}

	/// <inheritdoc cref="IApartmentRelationRepository.GetApartmentRelations(Guid)"/>
	public IQueryable<ApartmentRelation> GetApartmentRelations(Guid id)
	{
		var relations = _apartmentRelations.Where(relation => relation.Apartment.Id == id);
		return relations.AsQueryable();
	}

	private static List<ApartmentRelation> GenerateData()
	{
		return new List<ApartmentRelation>
		{
			new()
			{
				Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6") },
				RelationType = ApartmentRelationType.Ownership,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00002f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00100f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.OwnershipFamily,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00003f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00205f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.Renter,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00004f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00205f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.Ownership,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00005f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00102f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.OwnershipFamily,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00006f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.Renter,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00007f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00294f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.Ownership,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00008f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00055f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.OwnershipFamily,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00009f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00001f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.Renter,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00010f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00055f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.Ownership,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00011f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00100f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.OwnershipFamily,
				Created = DateTime.Now,
			},
			new()
			{
				Id = Guid.Parse("00012f64-5717-4562-b3fc-2c963f66afa6"),
				Apartment = new Apartment {Id = Guid.Parse("00294f64-5717-4562-b3fc-2c963f66afa6")},
				RelationType = ApartmentRelationType.Renter,
				Created = DateTime.Now,
			},
		};
	}
}
