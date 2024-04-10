using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace consoleServer
{
  internal class Program
  {
    private static int _listeningPort = 2000;
    static void Main(string[] args)
    {
      Console.WriteLine("Starting server...");
      var serverCertificate = getServerCert();
      var listener = new TcpListener(IPAddress.Any, _listeningPort);
      listener.Start();
      while (true)
      {
        using (var client = listener.AcceptTcpClient())
        using (var sslStream = new SslStream(client.GetStream(), false, ValidateCertificate))
        {
          sslStream.AuthenticateAsServer(serverCertificate,
             true, SslProtocols.Tls12, false);

          var inputBuffer = new byte[4096];
          var inputBytes = 0;
          while (inputBytes == 0)
          {
            inputBytes = sslStream.Read(inputBuffer, 0,
               inputBuffer.Length);
          }
          var inputMessage = Encoding.UTF8.GetString(inputBuffer,
             0, inputBytes);
          Console.WriteLine("GOT Data: {0}", inputMessage);
          if(inputMessage == "Q")
          {
            Console.WriteLine("Closing server...");
            break;
          }
        }
      }
    }
    static bool ValidateCertificate(Object sender,
      X509Certificate certificate, X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
      // For this sample under Windows 7 I also get
      // a remote cert not available error, so we
      // just do a return true here to signal that
      // we are trusting things. In the real world,
      // this would be very bad practice.
      return true;
      //if (sslPolicyErrors == SslPolicyErrors.None)
      //{ return true; }
      // we don't have a proper certificate tree
      //if (sslPolicyErrors ==
      //      SslPolicyErrors.RemoteCertificateChainErrors)
      //{ return true; }
      //return false;
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
