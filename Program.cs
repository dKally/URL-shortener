using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UrlShortenerApi", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrlShortenerApi v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins"); 
app.UseAuthorization();

app.MapGet("/{shortCode}", async (string shortCode, AppDbContext dbContext) =>
{
    var urlItem = await dbContext.Urls.FirstOrDefaultAsync(u => u.ShortCode == shortCode);

    if (urlItem == null)
    {
        return Results.NotFound("Link encurtado não encontrado.");
    }

    return Results.Redirect(urlItem.OriginalUrl, permanent: true);
});

app.MapControllers();

app.Run();