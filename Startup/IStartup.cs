public interface IStartup
{
    IConfiguration Configuration { get; }
    void Configure(WebApplication app, IWebHostEnvironment environment);
    void ConfigurationService(IServiceCollection services);
}