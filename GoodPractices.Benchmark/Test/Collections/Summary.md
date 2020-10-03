### ReturnYieldVsReturnList

General tip - if the size of a List is known upfront, then it should be initialized with proper capacity what prevents from unnecessary copying elements when default buffer size is exceeded.
In given example the best results (in terms of memory allocation) we get when using yield keyword and depending on Enumerator. This approach is better because we never have to prealloacte entire list with all values, but rather create value one by one when it's needed.
```
                     Method | NumberOfItems |        Mean |       Error |      StdDev | Gen 0/1k Op | Allocated Memory/Op |
--------------------------- |-------------- |------------:|------------:|------------:|------------:|--------------------:|
                Return_List |             1 |    63.73 ns |   1.1566 ns |   1.0253 ns |      0.0508 |                80 B |
 Return_List_Known_Capacity |             1 |    47.68 ns |   0.6970 ns |   0.5820 ns |      0.0457 |                72 B |
                      Yield |             1 |    35.02 ns |   0.7198 ns |   1.0323 ns |      0.0254 |                40 B |
                Return_List |            10 |   221.10 ns |   2.5827 ns |   2.4159 ns |      0.1423 |               224 B |
 Return_List_Known_Capacity |            10 |   107.01 ns |   4.9694 ns |   5.5235 ns |      0.0660 |               104 B |
                      Yield |            10 |   112.52 ns |   1.4678 ns |   1.3011 ns |      0.0254 |                40 B |
                Return_List |           100 |  1,133.5 ns |    22.39 ns |    39.22 ns |      0.7572 |              1192 B |
 Return_List_Known_Capacity |           100 |    746.3 ns |    17.00 ns |    15.90 ns |      0.2937 |               464 B |
                      Yield |           100 |    906.5 ns |    11.68 ns |    10.36 ns |      0.0248 |                40 B |
                Return_List |          1000 | 8,251.92 ns | 136.2985 ns | 120.8250 ns |      5.3406 |              8432 B |
 Return_List_Known_Capacity |          1000 | 7,019.67 ns | 167.6531 ns | 193.0696 ns |      2.5711 |              4064 B |
                      Yield |          1000 | 8,709.88 ns | 172.8103 ns | 177.4635 ns |      0.0153 |                40 B |
```

### ArrayPoolVsManualArray

General tip - renting array of from pool of given type is more performant than creating new array each time. Drawback is that code gets more complicated - if array is not returned (potentially because exception was thrown) it brings no benefit.
```
                 Method |      Mean |      Error |     StdDev |    Median | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
----------------------- |----------:|-----------:|-----------:|----------:|------------:|------------:|------------:|--------------------:|
             DateFormat | 310.82 ns |  2.3078 ns |  2.0458 ns | 310.68 ns |      0.0262 |           - |           - |               168 B |
    PooledStringBuilder | 256.63 ns |  0.8953 ns |  0.7936 ns | 256.71 ns |      0.0100 |           - |           - |                64 B |
 NonPooledStringBuilder | 343.66 ns | 13.2666 ns | 38.9087 ns | 331.74 ns |      0.0429 |           - |           - |               272 B |
             Stackalloc |  90.09 ns |  0.7251 ns |  0.6428 ns |  89.93 ns |      0.0101 |           - |           - |                64 B |
```

### IEnumerableVsNewSubListVsSpan
Processing sub collection with Span is optimal in terms of speed (CPU) and memory.
Less performant is IEnumerable, however it brings more flexibility (selecting items with Where allow for selecting items which normally are not sequenced one by one in original collection, whereas in Span approach items create together a contigious set). The worst results are when new sub collection is created.

```
                 Method |      Mean |    Error |     StdDev |    Median | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
----------------------- |----------:|---------:|-----------:|----------:|------------:|------------:|------------:|--------------------:|
  ProcessWithNewSubList | 495.23 ns | 9.890 ns | 25.1726 ns | 504.55 ns |      0.0405 |           - |           - |               256 B |
 ProcessWithIEnumerable | 458.99 ns | 3.286 ns |  3.0736 ns | 457.43 ns |      0.0067 |           - |           - |                48 B |
        ProcessWithSpan |  99.08 ns | 1.054 ns |  0.8804 ns |  99.00 ns |           - |           - |           - |                   - |
```