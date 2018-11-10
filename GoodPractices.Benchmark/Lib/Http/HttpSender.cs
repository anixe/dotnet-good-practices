using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GoodPractices.Benchmark.Lib.Http
{
  public class HttpSender : IDisposable
  {
    private readonly TcpClient cli;
    private readonly string host;
    private readonly int port;

    public HttpSender(string host, int port)
    {
      this.cli = new TcpClient(host, port);
      this.host = host;
      this.port = port;
    }

    public void Dispose()
    {
      if(this.cli != null)
      {
        this.cli.Dispose();
      }
    }

    public Stream SendReceive()
    {
      var stream = cli.GetStream();
      using(var tw = new StreamWriter(stream, Encoding.ASCII, 1024, true))
      {
        var httpW = new HttpWriter(tw);
        httpW.WriteRequestLine(HttpMethod.Get, HttpVersion.Version11, "/small");
        httpW.WriteHost(this.host);
        httpW.WriteConnectionClose();
        httpW.WriteEoH();
        httpW.Flush();
      }
      return stream;
    }
  }
}