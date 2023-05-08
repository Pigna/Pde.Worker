using Pde.Worker.Core.Models;

namespace Pde.Worker.Core.Services;

public interface IFakeDataService
{
    object GetFakerDataByType(FakeDataType requestType, int seed);
}