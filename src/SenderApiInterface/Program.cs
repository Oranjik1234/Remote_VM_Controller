using SenderApiInterface.Services;
using SenderApiInterface.RemoteAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SharedSecretOptions>(
	builder.Configuration.GetSection("SharedSecret"));

builder.Services.Configure<SharedSecretOptions>(opt =>
{
	opt.SharedSecret = builder.Configuration["SharedSecret"];
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IVmExecutor, MockVmExecutor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();