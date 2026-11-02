using App;
Console.OutputEncoding=System.Text.Encoding.UTF8;

new Student("Юрій",18).PrintInfo();new Student("Олена",19).PrintInfo();
var account=new BankAccount(1000m);Console.WriteLine($"Баланс: {account.Balance:F2}");account.Deposit(250m);Console.WriteLine($"Після поповнення: {account.Balance:F2}");
try{account.Balance=-100;}catch(ArgumentOutOfRangeException e){Console.WriteLine(e.Message);}
var rectangle=new Rectangle(5,3);Console.WriteLine($"Прямокутник {rectangle.Width} × {rectangle.Height}, площа {rectangle.Area}");rectangle.Width=8;Console.WriteLine($"Після зміни ширини площа {rectangle.Area}");
