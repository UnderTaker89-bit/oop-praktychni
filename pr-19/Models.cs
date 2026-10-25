namespace Practice19;
public abstract class Shape
{
 protected abstract double CalcArea();
 public double Area=>CalcArea();
 public void ShowArea()=>Console.WriteLine($"{GetType().Name}: площа = {CalcArea():F2}");
 protected static void Validate(double x){if(!double.IsFinite(x)||x<=0)throw new ArgumentOutOfRangeException(nameof(x));}
}
public class Circle:Shape
{
 public double Radius{get;}
 public Circle(double radius){Validate(radius);Radius=radius;}
 protected override double CalcArea()=>Math.PI*Radius*Radius;
}
public class Rectangle:Shape
{
 public double Width{get;}public double Height{get;}
 public Rectangle(double width,double height){Validate(width);Validate(height);Width=width;Height=height;}
 protected override double CalcArea()=>Width*Height;
}
public abstract class Employee
{
 public string Name{get;}
 protected Employee(string name){if(string.IsNullOrWhiteSpace(name))throw new ArgumentException("Вкажіть ім’я",nameof(name));Name=name;}
 protected abstract decimal CalcSalary();
 public decimal Salary=>CalcSalary();
 public void ShowInfo()=>Console.WriteLine($"{Name}: зарплата {CalcSalary():F2} грн");
}
public class FullTimeEmployee:Employee
{
 private readonly decimal salary;
 public FullTimeEmployee(string name,decimal salary):base(name){if(salary<0)throw new ArgumentOutOfRangeException(nameof(salary));this.salary=salary;}
 protected override decimal CalcSalary()=>salary;
}
public class PartTimeEmployee:Employee
{
 private readonly decimal rate,hours;
 public PartTimeEmployee(string name,decimal rate,decimal hours):base(name){if(rate<0||hours<0)throw new ArgumentOutOfRangeException(nameof(rate));this.rate=rate;this.hours=hours;}
 protected override decimal CalcSalary()=>checked(rate*hours);
}
