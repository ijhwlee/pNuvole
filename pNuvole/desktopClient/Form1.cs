using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desktopClient
{
  public partial class Form1 : Form
  {
    private static int _hostPort = 2000;
    private static string _hostName = "localhost";
    private static string ServerCertificateName = "hwleeSslSocketCertificate";
    private X509Certificate clientCertificate;
    private X509CertificateCollection clientCertificateCollection;
    public Form1()
    {
      InitializeComponent();
      clientCertificate = getServerCert();
      clientCertificateCollection = new
         X509CertificateCollection(new X509Certificate[]
         { clientCertificate });
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void buttonSend_Click(object sender, EventArgs e)
    {
      using (var client = new TcpClient(_hostName, _hostPort))
      using (var sslStream = new SslStream(client.GetStream(),
         false, ValidateCertificate))
      {
        sslStream.AuthenticateAsClient(ServerCertificateName,
           clientCertificateCollection, SslProtocols.Tls12, false);

        var outputMessage = textBoxMsg.Text;
        var outputBuffer = Encoding.UTF8.GetBytes(outputMessage);
        sslStream.Write(outputBuffer);
        Console.WriteLine("Sent: {0}", outputMessage);
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
