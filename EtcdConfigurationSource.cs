using System;
using Microsoft.Extensions.Configuration;

namespace ConfigClient
{
    public class EtcdConfigurationSource : IConfigurationSource
    {
        public EtcdConnectionOptions Options { get; set; }

        public EtcdConfigurationSource(EtcdConnectionOptions options)
        {
            this.Options = options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EtcdConfigurationProvider(this);
        }
    }
}