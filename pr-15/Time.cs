namespace Practice15;
// Знаковий відрізок часу. Компоненти від’ємного значення також від’ємні.
public struct Time
{
 private int _seconds;
 public int Hours{readonly get=>_seconds/3600;set=>_seconds=checked(value*3600+_seconds%3600);}
 public int Minutes{readonly get=>(_seconds%3600)/60;set{Validate(value);_seconds=checked(_seconds+(value-Minutes)*60);}}
 public int Seconds{readonly get=>_seconds%60;set{Validate(value);_seconds=checked(_seconds+value-Seconds);}}
 public Time(int seconds){_seconds=seconds;}
 public Time(int minutes,int seconds){Validate(seconds);_seconds=checked(minutes*60+seconds);}
 public Time(int hours,int minutes,int seconds){Validate(minutes);Validate(seconds);_seconds=checked(hours*3600+minutes*60+seconds);}
 private static void Validate(int component){if(component is < -59 or > 59)throw new ArgumentOutOfRangeException(nameof(component),"Хвилини та секунди мають бути від -59 до 59.");}
 public readonly double TotalHours()=>_seconds/3600.0;
 public readonly double TotalMinutes()=>_seconds/60.0;
 public readonly int TotalSeconds()=>_seconds;
 public override readonly string ToString(){long absolute=_seconds<0?-(long)_seconds:_seconds;return $"{(_seconds<0?"-":"")}{absolute/3600:D2}:{absolute%3600/60:D2}:{absolute%60:D2}";}
 public static Time operator +(Time a,Time b)=>new(checked(a._seconds+b._seconds));
 public static Time operator -(Time a,Time b)=>new(checked(a._seconds-b._seconds));
}
