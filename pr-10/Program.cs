using Practice10;
Console.OutputEncoding=System.Text.Encoding.UTF8;
Console.WriteLine("Практична 10 — власне перетворення та банківське округлення");
foreach(string text in new[]{"a","123","-2147483648","2147483647","2147483648","  +42  ","12.5","-"})
{bool success=NumberHelper.TryParseInt(text,out int value);Console.WriteLine($"\"{text}\" → {success}, value = {value}");}
foreach(float number in new[]{3.2f,1.6f,4.5f,5.5f,-4.5f,-5.5f,-1.6f,-0.5f})Console.WriteLine($"{number} → {NumberHelper.BankersRounding(number)}");
