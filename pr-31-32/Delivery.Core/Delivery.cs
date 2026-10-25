namespace Delivery.Core;
public class VolumeWeight
{
 public decimal Weight{get;set;}
 public decimal Length{get;set;}
 public decimal Width{get;set;}
 public decimal Height{get;set;}
 public decimal VolumetricWeight=>checked(Length*Width*Height)/4000m;
 public decimal ChargeableWeight=>Math.Max(Weight,VolumetricWeight);
 public void Validate(){if(Weight<=0||Length<=0||Width<=0||Height<=0)throw new ArgumentException("Вага та всі габарити мають бути додатними.");}
}
public enum DeliveryType{Department,Courier,ExpressCourier}
public interface IDeliveryCostStrategy{DeliveryType DeliveryType{get;}decimal Calculate(VolumeWeight volumeWeight);}
public class DepartmentDeliveryStrategy:IDeliveryCostStrategy
{public DeliveryType DeliveryType=>DeliveryType.Department;public decimal Calculate(VolumeWeight p){ArgumentNullException.ThrowIfNull(p);p.Validate();return 40m+10m*p.ChargeableWeight;}}
public class CourierDeliveryStrategy:IDeliveryCostStrategy
{public DeliveryType DeliveryType=>DeliveryType.Courier;public decimal Calculate(VolumeWeight p){ArgumentNullException.ThrowIfNull(p);p.Validate();return 80m+15m*p.ChargeableWeight;}}
public class ExpressCourierDeliveryStrategy:IDeliveryCostStrategy
{public DeliveryType DeliveryType=>DeliveryType.ExpressCourier;public decimal Calculate(VolumeWeight p){ArgumentNullException.ThrowIfNull(p);p.Validate();return 120m+24m*p.ChargeableWeight;}}
public interface IDeliveryCostService{decimal Calculate(VolumeWeight volumeWeight,DeliveryType deliveryType);}
public class EarlyDeliveryCostService:IDeliveryCostService
{
 public decimal Calculate(VolumeWeight p,DeliveryType type)=>type switch
 {DeliveryType.Department=>new DepartmentDeliveryStrategy().Calculate(p),DeliveryType.Courier=>new CourierDeliveryStrategy().Calculate(p),DeliveryType.ExpressCourier=>new ExpressCourierDeliveryStrategy().Calculate(p),_=>throw new ArgumentOutOfRangeException(nameof(type))};
}
public class DeliveryCostService:IDeliveryCostService
{
 private readonly Dictionary<DeliveryType,IDeliveryCostStrategy> strategies=new();
 public DeliveryCostService(IEnumerable<IDeliveryCostStrategy> strategies)
 {ArgumentNullException.ThrowIfNull(strategies);foreach(var strategy in strategies){ArgumentNullException.ThrowIfNull(strategy);this.strategies.Add(strategy.DeliveryType,strategy);}}
 public decimal Calculate(VolumeWeight p,DeliveryType type)
 {if(!strategies.TryGetValue(type,out var strategy))throw new ArgumentOutOfRangeException(nameof(type),"Стратегію не зареєстровано.");return strategy.Calculate(p);}
}
