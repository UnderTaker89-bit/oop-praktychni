namespace App;
public class Publisher
{
 public string Name{get;set;}
 public Publisher(string name){Name=name;}
 public Publisher(Publisher other){ArgumentNullException.ThrowIfNull(other);Name=other.Name;}
}
public class Book:ICloneable
{
 public string Title;
 public string Author;
 public int Year;
 public Publisher Publisher;
 public Book(string title,string author,int year,Publisher publisher)
 {ArgumentNullException.ThrowIfNull(publisher);Title=title;Author=author;Year=year;Publisher=publisher;}
  
 public Book(Book other)
 {ArgumentNullException.ThrowIfNull(other);Title=other.Title;Author=other.Author;Year=other.Year;Publisher=new Publisher(other.Publisher);}
  
 public object Clone()=>MemberwiseClone();
 public Book DeepClone()=>new Book(this);
 public override string ToString()=>$"{Title}; {Author}; {Year}; видавництво {Publisher.Name}";
}
