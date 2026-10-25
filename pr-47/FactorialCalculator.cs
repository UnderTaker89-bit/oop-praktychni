namespace Practice47;
public static class FactorialCalculator
{
 public static long Factorial(int number){if(number is <0 or >20)throw new ArgumentOutOfRangeException(nameof(number));long result=1;for(int i=2;i<=number;i++)result=checked(result*i);return result;}
 public static long[] Calculate(int[] numbers,bool print=true)
 {
  ArgumentNullException.ThrowIfNull(numbers);var input=(int[])numbers.Clone();
  foreach(int x in input)if(x is <0 or >20)throw new ArgumentOutOfRangeException(nameof(numbers));
  long[] results=new long[input.Length];Thread[] threads=new Thread[input.Length];
  for(int i=0;i<input.Length;i++)
  {
   int index=i;threads[i]=new Thread(()=>{results[index]=Factorial(input[index]);if(print)Console.WriteLine($"факторіал числа: {input[index]}! = {results[index]}");});
  }
  foreach(var thread in threads)thread.Start();
  foreach(var thread in threads)thread.Join();
  return results;
 }
}
