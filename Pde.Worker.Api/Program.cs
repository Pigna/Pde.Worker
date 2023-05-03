using Pde.Worker.Api.Extensions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPdeWorkerApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add hangfire dashboard
app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();