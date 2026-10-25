namespace Practice03;
public class Student
{
 public string Name;
 public int Age;
 private readonly int id;
 private static int nextId;
 public Student(string name,int age){Name=name;Age=age;id=Interlocked.Increment(ref nextId);}
 public void PrintInfo()=>Console.WriteLine($"Студент №{id}: {Name}, {Age} років");
}
public class BankAccount
{
 private decimal balance;
 public decimal Balance
 {
  get=>balance;
  set{if(value<=0)throw new ArgumentOutOfRangeException(nameof(value),"Баланс має бути додатним.");balance=value;}
 }
 public BankAccount(decimal initialBalance){Balance=initialBalance;}
 public void Deposit(decimal amount)
 {if(amount<=0)throw new ArgumentOutOfRangeException(nameof(amount),"Сума поповнення має бути додатною.");Balance=checked(Balance+amount);}
}
public class Rectangle
{
 private double width,height;
 public double Width{get=>width;set{Validate(value);width=value;}}
 public double Height{get=>height;set{Validate(value);height=value;}}
 public double Area=>Width*Height;
 public Rectangle(double width,double height){Width=width;Height=height;}
 private static void Validate(double x){if(!double.IsFinite(x)||x<=0)throw new ArgumentOutOfRangeException(nameof(x),"Розмір має бути скінченним додатним числом.");}
}
