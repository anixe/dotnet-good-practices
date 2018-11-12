### ReadFileTest

```
                                 Method |      Mean |     Error |    StdDev |     Gen 0 |   Gen 1 |  Allocated |
--------------------------------------- |----------:|----------:|----------:|----------:|--------:|-----------:|
                  Use_Only_StreamReader |  9.586 ms | 0.0486 ms | 0.0431 ms | 1468.7500 | 15.6250 | 9093.75 KB |
 Use_MemoryMappedFile_And_Stream_Reader | 18.718 ms | 0.2006 ms | 0.1778 ms | 1468.7500 |       - | 9090.32 KB |
                  Use_Custom_FastReader | 23.889 ms | 0.2138 ms | 0.2000 ms |         - |       - |    8.08 KB |
 ```