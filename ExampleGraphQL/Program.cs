using AirlineTicketSales;
using AirlineTicketSales.DAO;
using AirlineTicketSales.Data;
using AirlineTicketSales.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������������ CORS
builder.Services.AddCors();

// ��������� ��������� ���� ������ � �������������� SQL Server
builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ���������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ������������ �����������
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

// ��������� GraphQL ������ � Query � Mutation
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections()
    .AddSorting()
    .AddFiltering();

var app = builder.Build();

// ������� ��� ����������: ���������� Swagger ��� ������������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// ������ ��������� ��� ����������
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.UseWebSockets();

// ������ ������ � ���� ��� ������ ����������
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AirlineDbContext>();
    // dbContext?.Database.EnsureCreated(); // ����� ����������������� ��� �������� ����
    DataSeeder.SeedData(dbContext); // ������ �������� ������
}

// �������� GraphQL
app.MapGraphQL("/graphql");

app.Run();