
using API.Context;
using API.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

var config=builder.Configuration;

builder.Services.AddDbContextFactory<ElementoContext>(opt =>
    opt.UseSqlServer(config.GetConnectionString(ModelsShared.Constants.CONECTIONNAME)));

builder.Services.AddTransient(typeof(IDBService<>),typeof(DBService<>));

builder.Services.AddControllers().AddOData(opt => opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(500));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
