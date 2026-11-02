using App;
using System.Diagnostics;
Console.OutputEncoding=System.Text.Encoding.UTF8;
 
bool full=Array.IndexOf(args,"--full")>=0,force=Array.IndexOf(args,"--force-gc")>=0;
DummyObject.Verbose=Array.IndexOf(args,"--verbose")>=0;
int iterations=full?100:8,length=full?100000:2000;
Console.WriteLine($"Режим {(force?"GC.Collect раз на 2 ітерації":"автоматичний GC")}; {iterations} × {length} об’єктів");
Console.WriteLine("Ітерація;Керована пам’ять байт;Gen0;Gen1;Gen2;Створено;Фіналізовано");
var watch=Stopwatch.StartNew();
for(int i=1;i<=iterations;i++)
{
 new TaskRunner().Run(length);
 if(force&&i%2==0){GC.Collect();GC.WaitForPendingFinalizers();GC.Collect();}
 Console.WriteLine($"{i};{GC.GetTotalMemory(false)};{GC.CollectionCount(0)};{GC.CollectionCount(1)};{GC.CollectionCount(2)};{DummyObject.Counter};{DummyObject.DestroyedCounter}");
}
watch.Stop();Console.WriteLine($"Час мс: {watch.ElapsedMilliseconds}");
 
GC.Collect();GC.WaitForPendingFinalizers();GC.Collect();
Console.WriteLine($"Підсумок: створено {DummyObject.Counter}, фіналізовано {DummyObject.DestroyedCounter}; пам’ять {GC.GetTotalMemory(false)} байт");
