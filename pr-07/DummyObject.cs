using System.Runtime.CompilerServices;
namespace Practice07;
public class DummyObject
{
 public static int Counter;
 public static int DestroyedCounter;
 public static bool Verbose;
 public string Text{get;set;}
 public DummyObject(){Text=new string('a',5000);var count=Interlocked.Increment(ref Counter);if(Verbose)Console.WriteLine($"Кількість створених об’єктів — {count}");}
 ~DummyObject(){var count=Interlocked.Increment(ref DestroyedCounter);if(Verbose)Console.WriteLine($"Кількість фіналізованих об’єктів — {count}");}
}
public class TaskRunner
{
 [MethodImpl(MethodImplOptions.NoInlining)]
 public void Run(int length=2000)
 {
  if(length<=0)throw new ArgumentOutOfRangeException(nameof(length));
  var array=new DummyObject[length];for(var i=0;i<length;i++)array[i]=new DummyObject();
  GC.KeepAlive(array); // Масив залишається досяжним до завершення цього методу.
 }
}
