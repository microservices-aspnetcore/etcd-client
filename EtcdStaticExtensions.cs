using Microsoft.Extensions.Configuration;

namespace ConfigClient
{
    public static class EtcdStaticExtensions
    {
        public static IConfigurationBuilder AddEtcdConfiguration(this IConfigurationBuilder builder,
            EtcdConnectionOptions connectionOptions)
        {
            return builder.Add(new EtcdConfigurationSource(connectionOptions));
        }
    }

    public class EtcdConnectionOptions
    {
        public string[] Urls { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RootKey { get; set; }
    }
}