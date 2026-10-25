namespace Practice37;
public interface IRepository<T> where T:Model
{int Add(T entity);T? GetById(int id);List<T> GetAll();void Update(T entity);bool Delete(int id);}
public class GenericRepository<T>:IRepository<T> where T:Model
{
 private static readonly List<T> items=[];
 private static readonly object gate=new();
 private static long nextId=1;
 public int Add(T entity)
 {
  ArgumentNullException.ThrowIfNull(entity);lock(gate)
  {
   if(entity.Id<0)throw new ArgumentOutOfRangeException(nameof(entity));
   if(entity.Id==0)entity.Id=checked((int)nextId);
   if(items.Exists(x=>x.Id==entity.Id))throw new ArgumentException("Ідентифікатор уже існує",nameof(entity));
   nextId=Math.Max(nextId,(long)entity.Id+1);items.Add(entity);return entity.Id;
  }
 }
 public T? GetById(int id){lock(gate)return items.Find(x=>x.Id==id);}
 public List<T> GetAll(){lock(gate)return new List<T>(items);}
 public void Update(T entity){ArgumentNullException.ThrowIfNull(entity);lock(gate){int i=items.FindIndex(x=>x.Id==entity.Id);if(i<0)throw new KeyNotFoundException("Запис не знайдено");items[i]=entity;}}
 public bool Delete(int id){lock(gate){int i=items.FindIndex(x=>x.Id==id);if(i<0)return false;items.RemoveAt(i);return true;}}
}
