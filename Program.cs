using messenger.Data;
using messenger.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlite<MessagesContext>(connString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://192.168.0.202:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

app.UseCors("AllowAll");

app.UseSession();

app.MapMessagesEndpoints();
app.MapUsersEndpoints();

await app.MigrateDbAsync();

app.Run();
