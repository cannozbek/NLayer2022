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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//Hen�z service interface'ini implemente etmedik bu y�zden daha sonra ekleyece�iz.
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
