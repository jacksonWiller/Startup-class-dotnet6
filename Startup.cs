namespace StarupClass.net6;

public class Startup : IStartup
{

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigurationService(IServiceCollection services)
    {

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}

public interface IStartup
{
    IConfiguration Configuration { get; }
    void Configure(WebApplication app, IWebHostEnvironment environment);
    void ConfigurationService(IServiceCollection services);
}

public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<TStartup>
    (this WebApplicationBuilder WebAppBuilder)  
    where TStartup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup),WebAppBuilder.Configuration) as IStartup;
            if (startup == null) throw new ArgumentException("Class Startup.cs inválida!");

            startup.ConfigurationService(WebAppBuilder.Services);
            var app = WebAppBuilder.Build();
            startup.Configure(app, app.Environment);

            app.Run();

            return WebAppBuilder;

        }
}

