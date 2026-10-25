using Practice17;
Console.OutputEncoding=System.Text.Encoding.UTF8;
var grades=new StudentGrades();grades["Юрій"]=10;grades["Олена"]=12;
Console.WriteLine("Практична 17 — індексатор StudentGrades");Console.WriteLine($"Оцінка Юрія: {grades["Юрій"]}");grades["Юрій"]=11;
Console.WriteLine($"Марко є у списку: {grades.ContainsStudent("Марко")}");grades["Марко"]=9;
foreach(var (name,grade) in grades.GetAll())Console.WriteLine($"{name}: {grade}");
try{Console.WriteLine(grades["Невідомий"]);}catch(KeyNotFoundException e){Console.WriteLine(e.Message);}
