using System;
using WireMock.Logging;
using WireMock.Net.StandAlone;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace wiremock_console
{
  class Program
  {
    static void Main(string[] args)
    {
      int port;
      if (args.Length == 0 || !int.TryParse(args[0], out port))
        port = 8080;
      var settings = new FluentMockServerSettings
      {
        Port = port
      };

      var server = FluentMockServer.Start(settings);

      server.Given(Request.Create().WithUrl(u => u.Contains("medium_gz")).UsingGet())
        .RespondWith(Response.Create()
            .WithStatusCode(200)
            .WithHeader("Content-Type", "text/plain")
            .WithHeader("Content-Encoding", "gzip")
            .WithBodyFromFile("stations_medium.ion.gz", true));


      server.Given(Request.Create().WithUrl(u => u.Contains("small")).UsingGet())
        .RespondWith(Response.Create()
            .WithStatusCode(200)
            .WithHeader("Content-Type", "text/plain")
            .WithBodyFromFile("stations_small.ion", true));

      server.Given(Request.Create().WithUrl(u => u.Contains("large")).UsingGet())
        .RespondWith(Response.Create()
            .WithStatusCode(200)
            .WithHeader("Content-Type", "text/plain")
            .WithBodyFromFile("stations_large.ion", true));

      server.Given(Request.Create().WithUrl(u => u.Contains("medium")).UsingGet())
        .RespondWith(Response.Create()
            .WithStatusCode(200)
            .WithHeader("Content-Type", "text/plain")
            .WithBodyFromFile("stations_medium.ion", true));

      Console.WriteLine("Press any key to stop the server");
      Console.ReadKey();    
    }
  }
}