using BookingService.Business.Abstract;
using BookingService.Business.Concrete;
using BookingService.Business.Concrete.Mapper;
using BookingService.Cache.Concrete.Repository;
using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Concrete.EntityFramework;
using BookingService.DataAccess.Concrete.EntityFramework.Cache;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.0",
        Title = "Booking Service RESTful API",
        Contact = new OpenApiContact
        {
            Name = "Enes Arat",
            Url = new Uri("https://github.com/enesarat"),
            Email = "enes_arat@outlook.com"
        },
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddSingleton<RedisRepository>(sp =>
{
    return new RedisRepository(url: builder.Configuration[key: "CacheOptions:Url"]);
});

builder.Services.AddSingleton<IDatabase>(sp =>
{
    var redisService = sp.GetRequiredService<RedisRepository>();
    return redisService.GetDatabase(0);
});


builder.Services.AddScoped<IBookingService, BookingManager>();
builder.Services.AddScoped<IBookingsDAL, EfBookingsRepository>();

builder.Services.AddScoped<IAppartmentService, AppartmentManager>();
//builder.Services.AddScoped<IAppartmentsDAL, EfAppartmentsRepository>();
builder.Services.AddScoped <IAppartmentsDAL>(sp=>
{
    var appartmentRepository = new EfAppartmentsRepository();
    var redisRepository = sp.GetRequiredService<RedisRepository>();
    return new EfAppartmentRepositoryWithCache(appartmentRepository, redisRepository);
});



builder.Services.AddScoped<ICompanyService, CompanyManager>();
builder.Services.AddScoped<ICompanyDAL, EfCompanyRepository>();

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUsersDAL, EfUsersRepository>();

builder.Services.AddAutoMapper(typeof(MapProfile));


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
