namespace Practice51;
public static class NumberDistributor
{
 public static (List<int> Even,List<int> Odd) Split(int[] fullArray)
 {
  ArgumentNullException.ThrowIfNull(fullArray);var input=(int[])fullArray.Clone();
  List<int> evenList=[],oddList=[];int nextIndex=0;
  object indexGate=new(),evenGate=new(),oddGate=new();
  void Work()
  {
   while(true)
   {
    int number;
    lock(indexGate){if(nextIndex>=input.Length)return;number=input[nextIndex++];}
    if(number%2==0){lock(evenGate)evenList.Add(number);}
    else{lock(oddGate)oddList.Add(number);}
   }
  }
  Thread[] threads=[new Thread(Work),new Thread(Work),new Thread(Work)];
  foreach(var thread in threads)thread.Start();foreach(var thread in threads)thread.Join();
  return(evenList,oddList);
 }
}
