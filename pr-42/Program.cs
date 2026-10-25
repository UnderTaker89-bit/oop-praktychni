using System.Globalization;
Console.OutputEncoding=System.Text.Encoding.UTF8;
Console.WriteLine("Практична 42 — перехоплення винятків за типом та умовою");
while(true)
{
 Console.Write("Введіть ціле число Int32: ");string? text=Console.ReadLine();
 if(text is null){Console.WriteLine("Введення завершено.");break;}
 try
 {
  int value=Int32.Parse(text,NumberStyles.Integer,CultureInfo.InvariantCulture);
  Console.WriteLine($"Отримане число: {value}");break;
 }
 catch(FormatException) when(string.IsNullOrWhiteSpace(text)){Console.WriteLine("Значення не є числом: введення порожнє. Спробуйте знову.");}
 catch(FormatException){Console.WriteLine("Введене значення не є цілим числом. Спробуйте знову.");}
 catch(OverflowException){Console.WriteLine("Значення виходить за межі Int32 [-2147483648; 2147483647]. Спробуйте знову.");}
}
