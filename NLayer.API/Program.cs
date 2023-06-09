using Microsoft.EntityFrameworkCore;
using NLayer.Repository;
using System.Reflection;
using NLayer.Core.UnitOfWorks;
using NLayer.Core.Repositories;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using NLayer.Core.Services;
using NLayer.Service.Services;
using NLayer.Service.Mapping;
using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using NLayer.API.Filters;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Bu sat�r, controller hizmetlerini dependency injection (ba��ml�l�k enjeksiyonu) konteynerine ekler.
// options parametresi, modellerin do�rulay�c�lar�na kar�� do�rulama yapan bir global filtre eklemek i�in kullan�l�r.
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttirbute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());


// ASP.NET Core API'da, hatal� model durumunda varsay�lan olarak ModelState'�n d�nd�rd��� hatay� engellemek i�in bu se�ene�i ekliyoruz.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Dependency Injection i�lemleri
// UnitOfWork, IGenericRepository ve IService aray�zleri i�in GenericRepository ve Service s�n�flar�n� kullan�yoruz.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IProductRepository,ProductRepository >();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddAutoMapper(typeof(MapProfile));



//Eski s�r�mlerde start dosyas�n�n i�inde configrationstartta veriyorduk
//methodalar de�i�medi ancak progam.cs i�ine yaz�yoruz
builder.Services.AddDbContext<AppDbContext>(x =>
{ 
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //tip g�venli de�il yani biz bu de�eri bu �ekilde kulland���mz�da repositoryde bir isim de�i�kli�inde
        //bu kod patlar o y�zden bir alt taraftaki kodu kullanaca��z
        //option.MigrationAssembly("NLayer.Repository");
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
