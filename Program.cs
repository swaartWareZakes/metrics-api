var builder = WebApplication.CreateBuilder(args);

// Add support for controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Console.WriteLine("🚀 Hello from GitHub Actions! For Staging");


// app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ Add this so your HealthController works!
app.MapControllers();

app.Run();
