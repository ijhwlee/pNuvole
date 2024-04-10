using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace pNuvoleClient
{
  internal class Program
  {
    private static int _hostPort = 2000;
    private static string _hostName = "localhost";
    private static string ServerCertificateName = "hwleeSslSocketCertificate";
    static void Main(string[] args)
    {
      var clientCertificate = getServerCert();
      var clientCertificateCollection = new
         X509CertificateCollection(new X509Certificate[]
         { clientCertificate });

      while (true)
      {
        using (var client = new TcpClient(_hostName, _hostPort))
        using (var sslStream = new SslStream(client.GetStream(),
           false, ValidateCertificate))
        {
          sslStream.AuthenticateAsClient(ServerCertificateName,
             clientCertificateCollection, SslProtocols.Tls12, false);

          Console.Write("Input message(Q: for exit): ");
          var outputMessage = Console.ReadLine();
          //var outputMessage = "I has secure data";
          var outputBuffer = Encoding.UTF8.GetBytes(outputMessage);
          sslStream.Write(outputBuffer);
          Console.WriteLine("Sent: {0}", outputMessage);
          if (outputMessage == "Q")
          {
            Console.WriteLine("Closing connection...");
            break;
          }
        }
      }
    }
    static bool ValidateCertificate(Object sender,
       X509Certificate certificate, X509Chain chain,
       SslPolicyErrors sslPolicyErrors)
    {
      if (sslPolicyErrors == SslPolicyErrors.None)
      { return true; }
      // ignore chain errors as where self signed
      if (sslPolicyErrors ==
         SslPolicyErrors.RemoteCertificateChainErrors)
      { return true; }
      return false;
    }
    private static X509Certificate getServerCert()
    {
      X509Store store = new X509Store(StoreName.My,
         StoreLocation.CurrentUser);
      store.Open(OpenFlags.ReadOnly);

      X509Certificate2 foundCertificate = null;
      foreach (X509Certificate2 currentCertificate
         in store.Certificates)
      {
        if (currentCertificate.IssuerName.Name
           != null && currentCertificate.IssuerName.
           Name.Equals("CN=hwleeSslSocketCertificate"))
        {
          foundCertificate = currentCertificate;
          break;
        }
      }

      return foundCertificate;
    }
  }
}
