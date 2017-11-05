``` ini

BenchmarkDotNet=v0.10.10, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1770)
Processor=Intel Core i7-6700HQ CPU 2.60GHz (Skylake), ProcessorCount=8
Frequency=2531249 Hz, Resolution=395.0619 ns, Timer=TSC
.NET Core SDK=2.0.2
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
|          Method |     Mean |     Error |    StdDev |
|---------------- |---------:|----------:|----------:|
| 测试StringBuilder | 22.33 ns | 0.4956 ns | 1.2435 ns |
|    测试StringJoin | 69.04 ns | 1.3906 ns | 1.3007 ns |
|   测试SuperString | 24.26 ns | 0.3091 ns | 0.2741 ns |
