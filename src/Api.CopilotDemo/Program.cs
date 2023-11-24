var builder = WebApplication.CreateBuilder(args);

// Setup configuration
var configuration = builder.Configuration.GetSection("Configuration");
builder.Services.AddOptions<Config>().Bind(configuration);

// Add awesome services
builder.Services.AddSingleton<IAIService, AIService>();
builder.Services.AddSingleton<ISearchService, SearchService>();

// Add accessible controllers
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
            policy =>
            {
                policy.SetIsOriginAllowed(_ => true)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .WithExposedHeaders("access-control-allow-origin", "access-control-allow-methods", "access-control-allow-headers", "access-control-allow-credentials", "access-control-max-age");
            });
});
builder.Services.AddControllers();

// BUILD!
var app = builder.Build();
app.UseCors();
app.MapControllers();
app.Run();
