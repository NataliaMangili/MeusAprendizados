using KafkaEventBus.Interfaces;
using PetAdopt.Application.Events.Handlers;
using PetAdopt.Infrastructure.MongoLog;
using Nest;
using CrossCutting.Elasticsearch;
using FluentAssertions.Common;
using PetAdopt.Domain.Interfaces;
using PetAdopt.Infrastructure.External;


var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

//Section do AppSettings e classe de Config da Biblioteca do Kafka
var kafkaSettings = builder.Configuration.GetSection("KafkaSettings").Get<KafkaSettings>();
builder.Services.KafkaConfigurationDI(kafkaSettings!);

builder.Services.AddTransient<INotificationHandler<PetAdoptedSendEmailEvent>, PetAdoptedSendEmailEventHandler>();

builder.Services.AddDbContext<PetContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<NgoMapper>();

//builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureMongo(builder.Configuration);

// Elasticsearch
var elasticsearchConfig = builder.Configuration.GetSection("Elasticsearch");
var elasticUri = elasticsearchConfig["Uri"];
var defaultIndex = elasticsearchConfig["DefaultIndex"];

// Config cliente Elasticsearch
var settings = new Nest.ConnectionSettings(new Uri(elasticUri)).DefaultIndex(defaultIndex);
var elasticClient = new ElasticClient(settings);

builder.Services.AddSingleton<IElasticClient>(elasticClient);
builder.Services.AddSingleton<IElasticsearchService, ElasticsearchService>();

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

app.MapGet("/", async (IElasticClient client) =>
{
    var response = await client.PingAsync();
    if (response.IsValid)
    {
        return Results.Ok(new { Status = "Connected", NodeInfo = response.DebugInformation });
    }
    else
    {
        return Results.Problem("Failed to connect to Elasticsearch");
    }
});

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
