using Newtonsoft.Json;
using EmailService.IServices;
using EmailService.Services;
using EmailService.UserSecrets;
using EmailService.Singletons;

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

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("https://rb05emailservice.azurewebsites.net/",
                                "https://dinner-in-motion-project.ew.r.appspot.com/",
                                "https://rb05emailservice.azurewebsites.net/",
                                "http://dinner-in-motion-project.ew.r.appspot.com/")
                                .WithMethods("PUT", "DELETE", "GET", "POST");
            policy.AllowCredentials();
        });
    });

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

    SetupCors(app);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

void AddTransients(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ISendEmailService, SendEmailService>();
    
    builder.Services.AddSingleton<IHostedService, KafkaConsumerHandler>();
}

void LoadSecurity(WebApplicationBuilder builder)
{
    builder.Services.Configure<Security>(builder.Configuration.GetSection("Security"));
}

void SetupCors(WebApplication app)
{
    app.UseCors("MyPolicy");
}