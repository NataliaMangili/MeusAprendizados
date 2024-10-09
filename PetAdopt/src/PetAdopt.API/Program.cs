using Microsoft.EntityFrameworkCore;
using PetAdopt.Application;
using PetAdopt.Application.AutoMapper;
using PetAdopt.Infrastructure;
using PetAdopt.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PetContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<NgoMapper>();

builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigureInfrastructure(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
               builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PetContext>();
    dbContext.Database.Migrate(); // Aplica as migrações
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PetAdopt.API v1"));
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
