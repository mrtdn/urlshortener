using URLShortener.Data;
using URLShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<URLDBContext>();
builder.Services.AddScoped<URLRepository>();
builder.Services.AddScoped<URLShorteningService>();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable static file serving
app.UseStaticFiles();

app.UseHttpsRedirection();

// Use CORS
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
