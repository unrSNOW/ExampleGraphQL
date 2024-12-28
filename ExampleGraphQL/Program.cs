using AirlineTicketSales;
using AirlineTicketSales.DAO;
using AirlineTicketSales.Data;
using AirlineTicketSales.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавляем конфигурацию CORS
builder.Services.AddCors();

// Настройка контекста базы данных с использованием SQL Server
builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавление Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем репозитории
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

// Добавляем GraphQL сервер с Query и Mutation
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections()
    .AddSorting()
    .AddFiltering();

var app = builder.Build();

// Условие для разработки: подключаем Swagger для тестирования
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Прочие настройки для приложения
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.UseWebSockets();

// Сеедим данные в базе при старте приложения
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AirlineDbContext>();
    // dbContext?.Database.EnsureCreated(); // Можно раскомментировать для создания базы
    DataSeeder.SeedData(dbContext); // Сеедим тестовые данные
}

// Настроим GraphQL
app.MapGraphQL("/graphql");

app.Run();