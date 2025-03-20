using Microsoft.Extensions.Configuration;
using System;
public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> _instance = new Lazy<AppConfiguration>(() => new AppConfiguration());
    private readonly IConfigurationRoot _configuration;

    private AppConfiguration()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) 
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) 
            .Build();
    }

    public static AppConfiguration Instance => _instance.Value;

    public string GetSetting(string key)
    {
        return _configuration[key];
    }

    public T GetSection<T>(string sectionName) where T : new()
    {
        var section = new T();
        _configuration.GetSection(sectionName).Bind(section);
        return section;
    }
}
