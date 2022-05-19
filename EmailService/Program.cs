using Newtonsoft.Json;
using EmailService.IServices;
using EmailService.Services;
using EmailService.UserSecrets;

var builder = WebApplication.CreateBuilder(args);

SetupServices(builder);

LoadSecurity(builder);

SetupApp(builder);

void SetupServices(WebApplicationBuilder builder)
{
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    AddTransients(builder);
}

void SetupApp(WebApplicationBuilder builder)
{
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

void AddTransients(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ISendEmailService, SendEmailService>();
}

void LoadSecurity(WebApplicationBuilder builder)
{
    builder.Services.Configure<Security>(builder.Configuration.GetSection("Security"));
}