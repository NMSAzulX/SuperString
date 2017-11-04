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
| 测试StringBuilder | 23.55 ns | 0.3180 ns | 0.2819 ns |
|    测试StringJoin | 67.84 ns | 1.1584 ns | 1.0836 ns |
|   测试SuperString | 22.60 ns | 0.4989 ns | 1.3990 ns |
