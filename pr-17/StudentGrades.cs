namespace Practice17;
public class StudentGrades
{
 private readonly Dictionary<string,int> grades=new(StringComparer.OrdinalIgnoreCase);
 public int this[string name]
 {
  get{ValidateName(name);return grades.TryGetValue(name,out var grade)?grade:throw new KeyNotFoundException($"Студента «{name}» немає у списку.");}
  set{ValidateName(name);if(value is <1 or >12)throw new ArgumentOutOfRangeException(nameof(value),"Оцінка має бути від 1 до 12.");grades[name]=value;}
 }
 public bool ContainsStudent(string name){ValidateName(name);return grades.ContainsKey(name);}
 private static void ValidateName(string name){if(string.IsNullOrWhiteSpace(name))throw new ArgumentException("Вкажіть ім’я студента",nameof(name));}
 public IEnumerable<KeyValuePair<string,int>> GetAll()=>new Dictionary<string,int>(grades);
}
