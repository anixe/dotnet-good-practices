using System;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using ConsoleTools;

namespace GoodPractices.Benchmark
{
  class Program
  {
    static void Main(string[] args)
    {
      var config = ManualConfig.CreateEmpty()
        .With(JitOptimizationsValidator.DontFailOnError)
        .With(ConsoleLogger.Default)
        .With(MemoryDiagnoser.Default)
        .With(EnvironmentAnalyser.Default)
        .With(BenchmarkDotNet.Columns.DefaultColumnProviders.Instance);

      var menu = new ConsoleMenu();

      var testTypes = CollectTypes();

      menu.Add("All", () => RunBenchmark(testTypes, config));

      foreach (var type in testTypes.OrderBy(t => t.FullName))
      {
        menu.Add(TestName(type), () => RunBenchmark(type, config));
      }

      menu.Add("Exit", ConsoleMenu.Close);

      menu.Configure(menuConfig =>
        {
          menuConfig.ItemForegroundColor = ConsoleColor.White;
          menuConfig.SelectedItemForegroundColor = ConsoleColor.Green;
          menuConfig.ClearConsole = true;
        }
      );

      menu.Show();
    }

    private static string TestName(Type type)
    {
      return string.Join(".", type.FullName.Split(".").TakeLast(2));
    }

    private static void RunBenchmark(Type type, IConfig config)
    {
      BenchmarkRunner.Run(type, config);
      Console.WriteLine("Press any key to back to menu");
      Console.Read();
    }

    private static void RunBenchmark(Type[] types, IConfig config)
    {
      foreach (var type in types)
      {
        BenchmarkRunner.Run(type, config);
      }
    }

    private static Type[] CollectTypes()
    {
      var classesWithAttribute = typeof(BenchmarkDotNet.Attributes.BenchmarkAttribute);

      return Assembly.GetEntryAssembly().GetTypes()
          .Where(t => t.GetMethods().Any(m => m.GetCustomAttributes(classesWithAttribute, false).Length > 0))
          .OrderBy(c => c.Name)
          .ToArray();
    }
  }
}