using Practice47;
Console.OutputEncoding=System.Text.Encoding.UTF8;
Console.WriteLine("Практична 47 — факторіали у 20 потоках Thread");
int[] numbers=new int[20];for(int i=0;i<numbers.Length;i++)numbers[i]=Random.Shared.Next(1,13);
Console.WriteLine("Масив: "+string.Join(", ",numbers));FactorialCalculator.Calculate(numbers);
Console.WriteLine("Розрахунок закінчено.");
