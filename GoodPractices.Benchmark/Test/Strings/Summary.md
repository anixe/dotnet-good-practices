### StringConcatTest 
string concatenation benchmarks  
```
                                  Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
---------------------------------------- |----------:|----------:|----------:|-------:|----------:|
                       Contact_2_Strings |  27.28 ns | 0.3210 ns | 0.3003 ns | 0.0178 |     112 B |
                       Contact_2_Objects | 242.40 ns | 3.3588 ns | 3.1418 ns | 0.0267 |     168 B |
                       Contact_5_Strings | 104.00 ns | 0.3728 ns | 0.3488 ns | 0.0432 |     272 B |
                       Contact_6_Objects | 346.47 ns | 2.2163 ns | 1.9647 ns | 0.0658 |     416 B |
   Contact_6_Objects_Converted_To_String | 303.58 ns | 1.9592 ns | 1.8327 ns | 0.0467 |     296 B |
 Contact_3_3_Objects_Converted_To_String | 301.77 ns | 2.6010 ns | 2.3057 ns | 0.0529 |     336 B |
```

---

### StringBuilderAppendTest
string producing using StringBuilder. General tip is - if you can compute size of the final string upfront (it's just a sum of Length of every string that is a part of the result) it's best to initialize StringBuilder with the size.  
```
                                                Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
------------------------------------------------------ |----------:|----------:|----------:|-------:|----------:|
                                     Contact_2_Strings | 140.47 ns | 1.1367 ns | 1.0632 ns | 0.0725 |     456 B |
                     Contact_2_Strings_With_Initialize |  61.58 ns | 0.3874 ns | 0.3434 ns | 0.0495 |     312 B |
                                     Contact_2_Objects | 588.03 ns | 1.9429 ns | 1.8174 ns | 0.0772 |     488 B |
                     Contact_2_Objects_With_Initialize | 281.06 ns | 0.7949 ns | 0.7046 ns | 0.0467 |     296 B |
                                     Contact_5_Strings | 233.18 ns | 1.4020 ns | 1.3114 ns | 0.1180 |     744 B |
                      Contact_5_Strings_With_Initialie | 110.53 ns | 0.9180 ns | 0.8587 ns | 0.0838 |     528 B |
                                     Contact_6_Objects | 393.96 ns | 0.9677 ns | 0.8081 ns | 0.0787 |     496 B |
                     Contact_6_Objects_With_Initialize | 324.01 ns | 1.1170 ns | 0.9902 ns | 0.0758 |     480 B |
                 Contact_6_Objects_Converted_To_String | 390.98 ns | 1.7742 ns | 1.5728 ns | 0.0901 |     568 B |
 Contact_6_Objects_Converted_To_String_With_Initialize | 308.20 ns | 1.0112 ns | 0.9458 ns | 0.0873 |     552 B |
```

---

### PooledStringBuilderAppendTest
similar to StringBuilderAppendTest but this time StringBuilder instances are taken from the resuable pool. Crucial in this approach is to not forget to *RETURN* instance of StringBuilder back to pool. 
```

                               Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
------------------------------------- |----------:|----------:|----------:|-------:|----------:|
                     Append_2_Strings |  40.08 ns | 0.1206 ns | 0.1007 ns | 0.0178 |     112 B |
                     Append_2_Objects | 254.07 ns | 2.0901 ns | 1.9551 ns | 0.0148 |      96 B |
                     Append_6_Strings |  78.81 ns | 0.4790 ns | 0.4480 ns | 0.0317 |     200 B |
                     Append_6_Objects | 278.81 ns | 1.5673 ns | 1.4660 ns | 0.0238 |     152 B |
 Append_6_Objects_Converted_To_String | 273.15 ns | 1.8794 ns | 1.6660 ns | 0.0353 |     224 B |
```
---

### PooledStringBuilderCopyToTest ###
similar to PooledStringBuilderAppendTest, but this time we're copying chars from StringBuilder to pooled array of chars. Again, it's important to back both StringBuilder and rent char array to the pool.
Important thing to remember is that despite we have an array of let's say 1024 chars, in fact we can't assume all chars were written by our StringBuilder, so we must also have information of written chars.

Example:
``` c#
var sb = Consts.StringBuilderPool.Get();
var target = ArrayPool<char>.Shared.Rent(1024);

sb.Append("20_char_string_here");
sb.CopyTo(0, this.target, 0, sb.Length);

// notice that at this point target has size of 1024 chars, but only chars from 0..sb.Length are interesting for us. The rest of them is essentially a garbage - it's possible that this particular array was already used a couple of times and there are some chars written there.
// So if you need to rewrite these chars to another buffer, keep in mind you can't rewrite all the chars from the target (1024), but you need to remember number of written chars from StringBuilder instance.

Consts.StringBuilderPool.Return(this.sb);
ArrayPool<char>.Shared.Return(this.target);
```

```
                               Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
------------------------------------- |----------:|----------:|----------:|-------:|----------:|
                     Append_2_Strings |  42.50 ns | 0.1945 ns | 0.1819 ns |      - |       0 B |
                     Append_2_Objects | 247.10 ns | 0.7808 ns | 0.6921 ns |      - |       0 B |
                     Append_6_Strings |  80.49 ns | 0.4190 ns | 0.3919 ns |      - |       0 B |
                     Append_6_Objects | 286.08 ns | 0.8034 ns | 0.7515 ns |      - |       0 B |
 Append_6_Objects_Converted_To_String | 278.37 ns | 1.9551 ns | 1.6326 ns | 0.0110 |      72 B |
```
 
---

### RegexVsStringMatchingTest
Comparison of different ways of telling whether string mathes our pattern. 
```
                                                   Method |      Mean |      Error |     StdDev |  Gen 0 | Allocated |
-------------------------------------------------------- |----------:|-----------:|-----------:|-------:|----------:|
                             Regex_Pattern_Same_As_Input | 371.30 ns |  0.5025 ns |  0.4196 ns | 0.0329 |     104 B |
                     CompiledRegex_Pattern_Same_As_Input | 319.20 ns |  1.1929 ns |  0.9961 ns |      - |       0 B |
                Matcher_No_Storage_Pattern_Same_As_Input | 264.85 ns |  0.4867 ns |  0.4552 ns | 0.1116 |     352 B |
                   Matcher_Storage_Pattern_Same_As_Input |  71.86 ns |  0.0691 ns |  0.0613 ns | 0.0304 |      96 B |
                             Regex_Pattern_With_Wildcard | 294.92 ns |  0.1752 ns |  0.1553 ns | 0.0329 |     104 B |
                     CompiledRegex_Pattern_With_Wildcard | 333.34 ns |  0.1931 ns |  0.1806 ns |      - |       0 B |
                Matcher_No_Storage_Pattern_With_Wildcard | 219.13 ns |  0.2386 ns |  0.2232 ns | 0.1016 |     320 B |
                   Matcher_Storage_Pattern_With_Wildcard |  67.76 ns |  0.0396 ns |  0.0331 ns | 0.0254 |      80 B |
              Regex_Pattern_Has_More_Elements_Than_Input | 495.53 ns |  1.2283 ns |  1.1490 ns | 0.0324 |     104 B |
      CompiledRegex_Pattern_Has_More_Elements_Than_Input | 245.91 ns |  0.2380 ns |  0.2227 ns |      - |       0 B |
 Matcher_No_Storage_Pattern_Has_More_Elements_Than_Input | 197.30 ns |  0.1790 ns |  0.1587 ns | 0.1245 |     392 B |
    Matcher_Storage_Pattern_Has_More_Elements_Than_Input |  74.02 ns |  0.0609 ns |  0.0569 ns | 0.0330 |     104 B |
                  Regex_Pattern_With_Wildcard_At_The_End | 305.47 ns |  0.1344 ns |  0.1123 ns | 0.0329 |     104 B |
          CompiledRegex_Pattern_With_Wildcard_At_The_End | 355.19 ns |  0.3306 ns |  0.3092 ns |      - |       0 B |
     Matcher_No_Storage_Pattern_With_Wildcard_At_The_End | 448.67 ns |  0.1412 ns |  0.1321 ns | 0.1268 |     400 B |
        Matcher_Storage_Pattern_With_Wildcard_At_The_End |  71.40 ns |  1.4576 ns |  1.2921 ns | 0.0279 |      88 B |
               Regex_Pattern_With_Wildcard_In_The_Middle | 512.29 ns | 10.1139 ns | 11.2416 ns | 0.0324 |     104 B |
       CompiledRegex_Pattern_With_Wildcard_In_The_Middle | 640.40 ns |  5.4431 ns |  4.8252 ns |      - |       0 B |
  Matcher_No_Storage_Pattern_With_Wildcard_In_The_Middle | 308.34 ns |  2.2723 ns |  2.0144 ns | 0.1345 |     424 B |
     Matcher_Storage_Pattern_With_Wildcard_In_The_Middle |  74.48 ns |  0.1129 ns |  0.1057 ns | 0.0330 |     104 B |
                                 Regex_Pattern_Uppercase | 477.53 ns |  1.5648 ns |  1.3871 ns | 0.0324 |     104 B |
                         CompiledRegex_Pattern_Uppercase | 321.03 ns |  0.2113 ns |  0.1764 ns |      - |       0 B |
                    Matcher_No_Storage_Pattern_Uppercase | 268.71 ns |  0.3062 ns |  0.2864 ns | 0.1116 |     352 B |
                       Matcher_Storage_Pattern_Uppercase |  71.82 ns |  0.1074 ns |  0.1004 ns | 0.0304 |      96 B |
```