
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ModelsShared;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<List<Elemento>> ();

var config = builder.Configuration;
const string APIURL = "APIURL";

builder.Services.AddHttpClient(ModelsShared.Constants.APICLIENTNAME, c =>
{
    c.BaseAddress = new Uri(config.GetConnectionString(APIURL) ?? "");
});


var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
