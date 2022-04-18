using Cms.ContentArea;
using Cms.Content;
using Cms.Migrations;
using Cms.Shared;
using Cms.ContentTemplate;


Console.WriteLine("Running migrations");
SeedDatabase.RunMigrations();
Console.WriteLine("Done migrating");


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Cms", Version = "v1" });
});

builder.Services.AddShared();

builder.Services.AddContentArea();
builder.Services.AddContentTemplate();
builder.Services.AddContent();

builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cms v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
