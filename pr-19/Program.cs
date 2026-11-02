using App;
Console.OutputEncoding=System.Text.Encoding.UTF8;

Shape[] shapes=[new Circle(3),new Rectangle(4,5)];foreach(var shape in shapes)shape.ShowArea();
Employee[] employees=[new FullTimeEmployee("Олена",25000m),new PartTimeEmployee("Юрій",200m,80m)];foreach(var employee in employees)employee.ShowInfo();
if(Math.Abs(shapes[0].Area-9*Math.PI)>1e-10||shapes[1].Area!=20||employees[1].Salary!=16000m)throw new InvalidOperationException("Некоректний розрахунок");
Console.WriteLine("Розрахунки перевірено.");
