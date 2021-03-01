# CloneObjectTests

|                                                        Method |      Mean |    Error |   StdDev |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------------------------------------------- |----------:|---------:|---------:|--------:|------:|------:|----------:|
|                        Clone_UsingNewtonsoftJsonSerialization | 125.75 us | 1.123 us | 0.877 us | 15.1367 |     - |     - |  31.26 KB |
|       Clone_UsingNewtonsoftJsonSerialization_WithMemoryStream | 131.56 us | 1.535 us | 1.282 us |  8.0566 |     - |     - |  16.59 KB |
|      Clone_UsingNewtonsoftJsonSerialization_WithStringBuilder | 124.39 us | 1.252 us | 1.045 us | 10.9863 |     - |     - |  22.48 KB |
|                        Clone_UsingSystemTextJsonSerialization |  80.59 us | 0.748 us | 0.624 us |  7.5684 |     - |     - |  15.58 KB |
|          Clone_UsingSystemTextJsonSerialization_WithByteArray |  79.72 us | 0.414 us | 0.346 us |  5.9814 |     - |     - |  12.28 KB |
|       Clone_UsingSystemTextJsonSerialization_WithMemoryStream |  85.92 us | 0.492 us | 0.411 us |  8.5449 |     - |     - |   17.5 KB |
| Clone_UsingSystemTextJsonSerialization_WithMemoryStream_Async |  84.79 us | 0.915 us | 0.714 us |  4.3945 |     - |     - |   9.36 KB |