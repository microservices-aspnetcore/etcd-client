using System;
using System.Collections.Generic;
using EtcdNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace ConfigClient
{
    public class EtcdConfigurationProvider : ConfigurationProvider
    {
        private EtcdConfigurationSource source;

        public EtcdConfigurationProvider(EtcdConfigurationSource source)
        {
            this.source = source;
        }

        public override void Load()
        {
            EtcdClientOpitions options = new EtcdClientOpitions()
            {
                Urls = source.Options.Urls,
                Username = source.Options.Username,
                Password = source.Options.Password,
                UseProxy = false,
                IgnoreCertificateError = true
            };
            EtcdClient etcdClient = new EtcdClient(options);
            try
            {
                EtcdResponse resp = etcdClient.GetNodeAsync(source.Options.RootKey,
                    recursive: true, sorted: true).Result;
                if (resp.Node.Nodes != null)
                {
                    foreach (var node in resp.Node.Nodes)
                    {
                        // child node
                        Data[node.Key] = node.Value;
                    }
                }
            }
            catch (EtcdCommonException.KeyNotFound)
            {
                // key does not 
                Console.WriteLine("key not found exception");
            }
        }
    }
}