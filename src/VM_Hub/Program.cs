using VM_Hub.ConfigClasses;
using VM_Hub.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<HubSettings>(builder.Configuration.GetSection("AccessPassword"));

var app = builder.Build();

PasswordManager.SetPassword("default1234");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
