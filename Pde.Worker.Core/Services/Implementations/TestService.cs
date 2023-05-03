namespace Pde.Worker.Core.Services.Implementations;

public class TestService : ITestService
{
    public void Test(string message)
    {
        Console.WriteLine(message);
    }
}