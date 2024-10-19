using messenger.Data;
using messenger.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlite<MessagesContext>(connString);

var app = builder.Build();

app.MapMessagesEndpoints();
app.MapUsersEndpoints();

await app.MigrateDbAsync();

app.Run();
