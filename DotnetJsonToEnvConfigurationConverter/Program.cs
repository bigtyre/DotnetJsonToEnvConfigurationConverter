using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: false);

var configuration = builder.Build();

static void PrintConfiguration(IConfiguration configuration, string prefix = "")
{
    foreach (var section in configuration.GetChildren())
    {
        var key = string.IsNullOrEmpty(prefix) ? section.Key : $"{prefix}:{section.Key}";
        if (section.GetChildren().Any())
        {
            PrintConfiguration(section, key);
        }
        else
        {
            var envKey = key.Replace(":", "__").ToUpper();
            Console.WriteLine($"{envKey}={section.Value}");
        }
    }
}

PrintConfiguration(configuration);
