namespace Practice37;
public class Model{public int Id{get;set;}}
public class Posting:Model
{
 public string TrackingNumber{get;set;}="";public decimal Weight{get;set;}public string FromAddress{get;set;}="";public string ToAddress{get;set;}="";public string Status{get;set;}="Створено";
 public override string ToString()=>$"#{Id} {TrackingNumber}; {Weight} кг; {FromAddress} → {ToAddress}; {Status}";
}
public class ContactPerson:Model
{
 public string Name{get;set;}="";public string Phone{get;set;}="";public string Email{get;set;}="";
 public override string ToString()=>$"#{Id} {Name}; {Phone}; {Email}";
}
