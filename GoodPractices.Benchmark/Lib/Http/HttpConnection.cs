using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GoodPractices.Benchmark.Lib.Http
{
  public class HttpConnection
  {
    private readonly TcpClient client;
    private readonly IPHostEntry ipHostInfo;
    private int port;

    public HttpConnection(string host, int port = 80)
      :this(Dns.GetHostEntry(host), port)
    {
    }

    public HttpConnection(IPHostEntry ipHostInfo, int port = 80)
    {
      this.port = port;
      this.ipHostInfo = ipHostInfo;
      this.client = new TcpClient();
    }

    public Task ConnectAsync()
    {
      return this.client.ConnectAsync(this.ipHostInfo.AddressList, this.port);
    }

    public void Close()
    {
      this.client.Close();
    }

  }
}