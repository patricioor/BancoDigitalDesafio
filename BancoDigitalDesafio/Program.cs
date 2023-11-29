using BancoDigitalDesafio.Services.Interfaces;
using BancoDigitalDesafio.Services.Refit;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITransactionAuthorizationIntegration, TransactionAuthorizationIntegration>();
builder.Services.AddRefitClient<ITransactionAuthorizationRefit>()
    .ConfigureHttpClient(
        x =>
        {
            x.BaseAddress = new Uri("https://run.mocky.io");
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();