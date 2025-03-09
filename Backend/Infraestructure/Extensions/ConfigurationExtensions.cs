using Microsoft.Extensions.Configuration;

namespace Infraestructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("IsUnitTest");
        }
        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection")!;
        }
    }
}
