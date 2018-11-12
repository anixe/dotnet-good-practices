using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using ConsoleTools;
using GoodPractices.Benchmark.Lib.Http;
using GoodPractices.Benchmark.Test.Collections;

namespace GoodPractices.Benchmark
{
  class Program
  {
    static void Main(string[] args)
    {
      var config = ManualConfig.CreateEmpty()
        .With(JitOptimizationsValidator.DontFailOnError)
        .With(MemoryDiagnoser.Default)
        .With(ConsoleLogger.Default);
      
      var menu = new ConsoleMenu();

      var testTypes = CollectTypes();

      menu.Add("All", () => RunBenchmark(testTypes, config));

      foreach (var type in testTypes)
      {
        menu.Add(type.Name, () => RunBenchmark(type, config));          
      }

      menu.Configure(menuConfig => 
        {
          menuConfig.ItemForegroundColor = ConsoleColor.White;
          menuConfig.SelectedItemForegroundColor = ConsoleColor.Green;
          menuConfig.ClearConsole = true;
        }
      );

      menu.Show();
    }

    private static void RunBenchmark(Type type, IConfig config)
    {
      BenchmarkRunner.Run(type, config);
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