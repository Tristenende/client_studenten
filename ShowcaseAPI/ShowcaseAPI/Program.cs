using Microsoft.EntityFrameworkCore;
using ShowcaseAPI.DataContext;
using ShowcaseAPI.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("ConnStr")));

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder => {
            builder
                .AllowAnyOrigin()
                .WithMethods("POST")
                .WithMethods("GET")
                .AllowAnyHeader();
        });
});

builder.Services.AddHttpClient();
// Add services to the container.

builder.Services.AddControllers();
// Add scope
builder.Services.AddScoped<MailService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
