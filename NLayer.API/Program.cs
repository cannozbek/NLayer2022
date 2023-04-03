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
// Bu satýr, controller hizmetlerini dependency injection (baðýmlýlýk enjeksiyonu) konteynerine ekler.
// options parametresi, modellerin doðrulayýcýlarýna karþý doðrulama yapan bir global filtre eklemek için kullanýlýr.
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttirbute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());


// ASP.NET Core API'da, hatalý model durumunda varsayýlan olarak ModelState'ýn döndürdüðü hatayý engellemek için bu seçeneði ekliyoruz.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Dependency Injection iþlemleri
// UnitOfWork, IGenericRepository ve IService arayüzleri için GenericRepository ve Service sýnýflarýný kullanýyoruz.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IProductRepository,ProductRepository >();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddAutoMapper(typeof(MapProfile));



//Eski sürümlerde start dosyasýnýn içinde configrationstartta veriyorduk
//methodalar deðiþmedi ancak progam.cs içine yazýyoruz
builder.Services.AddDbContext<AppDbContext>(x =>
{ 
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //tip güvenli deðil yani biz bu deðeri bu þekilde kullandýðýmzýda repositoryde bir isim deðiþkliðinde
        //bu kod patlar o yüzden bir alt taraftaki kodu kullanacaðýz
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
