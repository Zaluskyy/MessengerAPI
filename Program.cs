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
            builder.WithOrigins("http://localhost:3000", "http://192.168.0.136:3000") // Adres twojej aplikacji React
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Umożliwia przesyłanie ciasteczek
        });
});
// Dodanie pamięci podręcznej do DI
builder.Services.AddDistributedMemoryCache(); // Używamy pamięci lokalnej

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = false; // Możesz ustawić to na true dla większego bezpieczeństwa
    options.Cookie.IsEssential = true; // Umożliwia korzystanie z sesji nawet jeśli użytkownik nie zaakceptuje ciasteczek
});



var app = builder.Build();

app.UseCors("AllowAll");

app.UseSession();

app.MapMessagesEndpoints();
app.MapUsersEndpoints();

await app.MigrateDbAsync();

app.Run();
