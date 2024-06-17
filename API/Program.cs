using Microsoft.EntityFrameworkCore;
using API.Infratructure.Data;
using API.Infratructure.Services;
using API.Application.Handlers.PrioridadesHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<MongoDbContext>(sp =>
    new MongoDbContext(
        builder.Configuration.GetConnectionString("MongoConnection")!,
        builder.Configuration["ConnectionStrings:MongoDatabaseName"]!));

builder.Services.AddSingleton<MongoDbInitializer>();

// Configure MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CrearPrioridadCommandHandler).Assembly));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Initialize MongoDB
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<MongoDbInitializer>();
    initializer.InitializeAsync().GetAwaiter().GetResult();
}

app.Run();
