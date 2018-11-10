using System;
using System.IO;
using System.Linq;

namespace GoodPractices.Benchmark
{
  internal static class FileLoader
  {
    public readonly static string ExamplesFileFolderPath;
    public readonly static string ProjectRootDir;

    static FileLoader()
    {
      ProjectRootDir = LookupForProjectRoot();
      ExamplesFileFolderPath = Path.Combine(ProjectRootDir, "Examples");
    }

    private static string LookupForProjectRoot()
    {
      var slnDir = GetSlnDir();
      if (!string.IsNullOrEmpty(slnDir))
      {
        return Path.Combine(slnDir, "GoodPractices.Benchmark");
      }
      throw new Exception("Cannot find sln dir.");
    }

    private static string GetSlnDir()
    {
      var currDir = Environment.CurrentDirectory;
      if (Directory.EnumerateFiles(currDir, "*.sln").Any())
      {
        return currDir;
      }
      while (Directory.GetParent(currDir) != null && !Directory.EnumerateFiles(currDir, "*.sln").Any())
      {
        currDir = Directory.GetParent(currDir).FullName;
      }
      return currDir;
    }

    public static string GetStationsIonPath()
    {
      return Path.Combine(ExamplesFileFolderPath, "stations.ion");
    }
  }
}