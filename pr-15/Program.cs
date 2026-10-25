using Practice15;
Console.OutputEncoding=System.Text.Encoding.UTF8;
var a=new Time(2,30,0);var b=new Time(1,45,30);var c=new Time(90);var d=new Time(3,15);
Console.WriteLine("Практична 15 — структура Time");Console.WriteLine($"{a} + {b} = {a+b}");Console.WriteLine($"{a} - {b} = {a-b}");Console.WriteLine($"{b} - {a} = {b-a}");Console.WriteLine($"90 секунд: {c}; 3 хв 15 с: {d}");
Console.WriteLine($"{a}: {a.TotalHours()} год; {a.TotalMinutes()} хв; {a.TotalSeconds()} с");
a.Hours=4;a.Minutes=10;a.Seconds=20;Console.WriteLine($"Після зміни властивостей: {a}");
