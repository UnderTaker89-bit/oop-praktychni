using Practice51;
Console.OutputEncoding=System.Text.Encoding.UTF8;
Console.WriteLine("Практична 51 — розподіл масиву у трьох потоках");
int[] fullArray=new int[1000];for(int i=0;i<fullArray.Length;i++)fullArray[i]=Random.Shared.Next(1,11);
var(evenList,oddList)=NumberDistributor.Split(fullArray);
Console.WriteLine($"Парні елементи ({evenList.Count}): "+string.Join(", ",evenList));
Console.WriteLine($"Непарні елементи ({oddList.Count}): "+string.Join(", ",oddList));
Console.WriteLine($"Усього розподілено: {evenList.Count+oddList.Count} із {fullArray.Length}");
