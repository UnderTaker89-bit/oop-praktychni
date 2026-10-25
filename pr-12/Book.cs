namespace Practice12;
public class Book
{
 public string Title{get;}
 public string Author{get;}
 public int Year{get;}
 public Book():this("Без назви","Невідомий автор",2000){}
 public Book(string title):this(title,"Невідомий автор",2000){}
 public Book(string title,string author,int year)
 {
  if(string.IsNullOrWhiteSpace(title))throw new ArgumentException("Вкажіть назву",nameof(title));
  if(string.IsNullOrWhiteSpace(author))throw new ArgumentException("Вкажіть автора",nameof(author));
  if(year<1||year>9999)throw new ArgumentOutOfRangeException(nameof(year));
  Title=title;Author=author;Year=year;
 }
 public void DisplayInfo()=>Console.WriteLine($"«{Title}», {Author}, {Year} рік");
}
