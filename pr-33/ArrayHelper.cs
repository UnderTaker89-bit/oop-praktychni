namespace Practice33;
public static class ArrayHelper
{
 public static int IndexOf<T>(T[] array,T value)
 {ArgumentNullException.ThrowIfNull(array);var equality=EqualityComparer<T>.Default;for(int i=0;i<array.Length;i++)if(equality.Equals(array[i],value))return i;return -1;}
 public static void Reverse<T>(T[] array)
 {ArgumentNullException.ThrowIfNull(array);for(int left=0,right=array.Length-1;left<right;left++,right--)(array[left],array[right])=(array[right],array[left]);}
 public static T Min<T>(T[] array) where T:IComparable<T>
 {Validate(array);T result=array[0];var comparer=Comparer<T>.Default;for(int i=1;i<array.Length;i++)if(comparer.Compare(array[i],result)<0)result=array[i];return result;}
 public static T Max<T>(T[] array) where T:IComparable<T>
 {Validate(array);T result=array[0];var comparer=Comparer<T>.Default;for(int i=1;i<array.Length;i++)if(comparer.Compare(array[i],result)>0)result=array[i];return result;}
 private static void Validate<T>(T[] array){ArgumentNullException.ThrowIfNull(array);if(array.Length==0)throw new ArgumentException("Масив не може бути порожнім",nameof(array));}
}
