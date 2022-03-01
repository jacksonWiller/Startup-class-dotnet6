namespace StarupClass.net6;
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