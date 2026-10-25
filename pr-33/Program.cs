using Practice33;
Console.OutputEncoding=System.Text.Encoding.UTF8;
Console.WriteLine("Практична 33 — узагальнені методи без LINQ");
int[] numbers=[7,2,9,2,4];string[] words=["banana","apple","pear","apple"];
Console.WriteLine("Числа: "+string.Join(", ",numbers));Console.WriteLine($"IndexOf(2) = {ArrayHelper.IndexOf(numbers,2)}, Min = {ArrayHelper.Min(numbers)}, Max = {ArrayHelper.Max(numbers)}");ArrayHelper.Reverse(numbers);Console.WriteLine("Навпаки: "+string.Join(", ",numbers));
Console.WriteLine("Рядки: "+string.Join(", ",words));Console.WriteLine($"IndexOf(apple) = {ArrayHelper.IndexOf(words,"apple")}, Min = {ArrayHelper.Min(words)}, Max = {ArrayHelper.Max(words)}");ArrayHelper.Reverse(words);Console.WriteLine("Навпаки: "+string.Join(", ",words));
Console.WriteLine($"Відсутній елемент: {ArrayHelper.IndexOf(words,"plum")}");
