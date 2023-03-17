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
//Henüz service interface'ini implemente etmedik bu yüzden daha sonra ekleyeceðiz.
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
