Anixe's good patterns of writing .NET code (mostly focused on performance).

### Tools
- [Anixe.IO](https://www.nuget.org/packages/Anixe.IO/) - library for fast operations

- [Microsoft.IO.RecyclableMemoryStream](https://github.com/Microsoft/Microsoft.IO.RecyclableMemoryStream) - pooling for MemoryStream object

- [Microsoft.Extensions.ObjectPool](https://github.com/aspnet/Extensions/tree/master/src/ObjectPool) - pooling for objects 

- [BenchmarkDotNet](https://benchmarkdotnet.org/articles/overview.html) - multiplatform benchmark tool

### Articles, Books
- http://mattwarren.org/2016/02/17/adventures-in-benchmarking-memory-allocations/
- http://mattwarren.org/2018/01/22/Resources-for-Learning-about-.NET-Internals/
- https://www.writinghighperf.net/

### Repo structure
```
project
│
└───GoodPractices.Benchmark
|   |  
|   └───Examples // data files used by benchmarks: json, xml, ion, txt 
|   └───Lib // utility code 
|   └───Test // benchmark files 
|       └───Collections  
|       └───Files  
|       └───Http  
|       └───Misc  
|       └───Strings  
```

### How to add new benchmark
- Add class with benchmark to proper namespace under GoodPractices.Benchmark/Test - if namespace doesn't exist yet just create new one. Alternatively you could keep file under GoodPractices.Benchmark/Test/Misc (discouraged)

- Add/Update Summary.md in selected namespace

### How to run
```
dotnet run -c Release
```