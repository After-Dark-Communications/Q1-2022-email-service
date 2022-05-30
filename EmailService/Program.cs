using Newtonsoft.Json;
using EmailService.IServices;
using EmailService.Services;
using EmailService.UserSecrets;

string allowOriginName = "AllowCors";
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

    SetupCors(builder);

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

    app.UseCors(allowOriginName);

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

void SetupCors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: allowOriginName,
                          policy =>
                          {
                              policy.WithOrigins("https://dinner-in-motion-project.ew.r.appspot.com",
                                                  "https://www.q1-2022-frontend.vercel.app");
                          });
    });
}