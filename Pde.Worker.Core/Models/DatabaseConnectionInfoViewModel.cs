namespace Pde.Worker.Core.Models;

public class DatabaseConnectionInfoViewModel
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public string Port { get; set; } = null!;
    public string Database { get; set; } = null!;
}