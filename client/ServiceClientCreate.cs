using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class ServiceClientCreate
    {
        static public ServiceClient createClient()
        {
            ServiceClient client;
            try
            {
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);

                binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

                string uri = "net.tcp://localhost:9002/AntivirusZMService";

                EndpointAddress endpoint = new EndpointAddress(new Uri(uri));

                client = new ServiceClient(binding, endpoint);

                client.ClientCredentials.Windows.ClientCredential.Domain = "";
            }
            catch (Exception _ex)
            {
                client = null;
            }
            return client;
        }
    }
}
