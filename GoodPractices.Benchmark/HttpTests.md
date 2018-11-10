
--------------------------------------------------------------------- |-----------:|---------:|---------:|---------:|---------:|---------:|-----------:|
                                         HttpClient_ReadAsStreamAsync | 2,577.9 us | 35.83 us | 33.51 us | 496.0938 | 496.0938 | 496.0938 |    1.79 KB |
                     HttpClient_ReadAsStreamAsync_ResponseHeadersRead | 1,264.1 us | 23.53 us | 22.01 us |        - |        - |        - |     1.7 KB |
 HttpClient_ReadAsStreamAsync_ResponseHeadersRead_ManualDecompression |   808.0 us | 21.92 us | 26.10 us |        - |        - |        - |    1.94 KB |
 HttpClient_LoadIntoBufferAsync_ReadAsStreamAsync_ResponseHeadersRead | 2,509.1 us | 11.10 us | 10.38 us | 496.0938 | 496.0938 | 496.0938 | 2012.57 KB |



                                             Method |     Mean |     Error |    StdDev |    Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
--------------------------------------------------- |---------:|----------:|----------:|---------:|---------:|---------:|-----------:|
                       HttpClient_ReadAsStreamAsync | 1.246 ms | 0.0196 ms | 0.0183 ms |        - |        - |        - |    1.85 KB |
         HttpClient_ReadAsStreamAsync_MultipleReads | 2.604 ms | 0.0579 ms | 0.1143 ms | 496.0938 | 496.0938 | 496.0938 | 2012.72 KB |
 HttpClient_ReadAsStreamAsync_MultipleReads_Gzipped | 6.075 ms | 0.0997 ms | 0.0884 ms | 492.1875 | 492.1875 | 492.1875 | 1815.25 KB |


                                                  Method |     Mean |     Error |    StdDev |   Median | Allocated |
-------------------------------------------------------- |---------:|----------:|----------:|---------:|----------:|
 Shared_HttpClient_ReadAsStreamAsync_ResponseHeadersRead | 1.312 ms | 0.0281 ms | 0.0770 ms | 1.287 ms |   2.65 KB |


