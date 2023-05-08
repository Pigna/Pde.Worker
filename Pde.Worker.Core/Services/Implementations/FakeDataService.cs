using Bogus;
using Pde.Worker.Core.Models;

namespace Pde.Worker.Core.Services.Implementations;

public class FakeDataService : IFakeDataService
{
    /// <summary>
    /// Get fake data by given FakeDataType
    /// </summary>
    /// <param name="requestType">FakeDataType</param>
    /// <param name="seed">Is the seed to base the generated fake data on.</param>
    /// <returns>An object with generated fake data based on the given seed.</returns>
    public object GetFakerDataByType(FakeDataType requestType, int seed)
    {
        var faker = new Faker
        {
            Random = new Randomizer(seed)
        };
        switch (requestType)
        {
            case FakeDataType.Account:
                return faker.Finance.Account();
            case FakeDataType.Amount:
                return faker.Finance.Amount();
            case FakeDataType.Number:
                return faker.Finance.Random.Number(0, 100);
            case FakeDataType.Avatar:
                //TODO: Avatar or profile picture
                break;
            case FakeDataType.BuildingNumber:
                return faker.Address.BuildingNumber();
            case FakeDataType.City:
                return faker.Address.City();
            case FakeDataType.Country:
                return faker.Address.Country();
            case FakeDataType.CreditCardNumber:
                return faker.Finance.CreditCardNumber();
            case FakeDataType.DateFuture:
                return faker.Date.Future();
            case FakeDataType.DatePast:
                return faker.Date.Past();
            case FakeDataType.Email:
                return faker.Internet.Email();
            case FakeDataType.FirstName:
                return faker.Name.FirstName();
            case FakeDataType.FullAddress:
                return faker.Address.FullAddress();
            case FakeDataType.FullName:
                return faker.Name.FullName();
            case FakeDataType.Iban:
                return faker.Finance.Iban();
            case FakeDataType.LastName:
                return faker.Name.LastName();
            case FakeDataType.Latitude:
                return faker.Address.Latitude();
            case FakeDataType.Longitude:
                return faker.Address.Longitude();
            case FakeDataType.Month:
                return faker.Date.Month();
            case FakeDataType.MonthNumber:
                return faker.Random.Number(1, 12);
            case FakeDataType.Password:
                return faker.Internet.Password();
            case FakeDataType.PhoneNumber:
                return faker.Phone.PhoneNumber();
            case FakeDataType.Picture:
                return faker.Image.PicsumUrl();
            case FakeDataType.State:
                return faker.Address.State();
            case FakeDataType.StreetAddress:
                return faker.Address.StreetAddress();
            case FakeDataType.StreetName:
                return faker.Address.StreetName();
            case FakeDataType.UserName:
                return faker.Internet.UserName();
            case FakeDataType.Zipcode:
                return faker.Address.ZipCode();
            case FakeDataType.Null:
            default:
                return null!;
        }

        return null!;
    }
}