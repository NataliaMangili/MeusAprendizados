using KafkaEventBus.Services;
using KafkaEventBus.Configurations;
using KafkaEventBus.Interfaces;
using PetAdopt.Application.Events.DTOs;
using PetAdopt.Application.Events.Handlers;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

//Section do AppSettings e classe de Config da Biblioteca do Kafka
var kafkaSettings = builder.Configuration.GetSection("KafkaSettings").Get<KafkaSettings>();
builder.Services.KafkaConfigurationDI(kafkaSettings!);

builder.Services.AddTransient<INotificationHandler<PetAdoptedSendEmailEvent>, PetAdoptedSendEmailEventHandler>();

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

var eventConsumer = app.Services.GetRequiredService<IEventConsumer>();
eventConsumer.Consume<PetAdoptedSendEmailEvent>("pet-adopted-topic", async (message) =>
{
    var mediator = app.Services.GetRequiredService<IMediator>();
    await mediator.Publish(message); // Publica o evento para ser tratado pelo handler
});


app.MapControllers();

app.Run();
