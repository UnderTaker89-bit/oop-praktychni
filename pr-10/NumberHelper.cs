namespace Practice10;
public static class NumberHelper
{
 public static bool TryParseInt(string? text,out int value)
 {
  value=0;if(text is null)return false;text=text.Trim();if(text.Length==0)return false;
  int i=0;bool negative=false;
  if(text[0]=='+'||text[0]=='-'){negative=text[0]=='-';i++;}
  if(i==text.Length)return false;
  long result=0,limit=negative?2147483648L:2147483647L;
  for(;i<text.Length;i++)
  {
   char c=text[i];if(c<'0'||c>'9')return false;int digit=c-'0';
   if(result>(limit-digit)/10)return false;
   result=result*10+digit;
  }
  value=(int)(negative?-result:result);return true;
 }
 public static int BankersRounding(float num)
 {
  if(!float.IsFinite(num)||((double)num)<int.MinValue||((double)num)>int.MaxValue)
   throw new ArgumentOutOfRangeException(nameof(num),"Потрібне скінченне число в діапазоні Int32.");
  int integer=(int)num;double fraction=(double)num-integer;
  double distance=fraction<0?-fraction:fraction;
  if(distance>0.5||(distance==0.5&&integer%2!=0))integer=checked(integer+(num<0?-1:1));
  return integer;
 }
}
