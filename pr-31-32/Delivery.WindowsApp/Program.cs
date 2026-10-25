using Delivery.Core;
namespace Delivery.WindowsApp;
internal static class Program
{
 [STAThread] static void Main(){ApplicationConfiguration.Initialize();Application.Run(new DeliveryForm());}
}
public class DeliveryForm:Form
{
 private readonly NumericUpDown weight=new(){Name="Weight",DecimalPlaces=3,Minimum=.001m,Maximum=1000,Value=5};
 private readonly NumericUpDown length=new(){Name="Length",DecimalPlaces=1,Minimum=.1m,Maximum=1000,Value=20};
 private readonly NumericUpDown width=new(){Name="Width",DecimalPlaces=1,Minimum=.1m,Maximum=1000,Value=40};
 private readonly NumericUpDown height=new(){Name="Height",DecimalPlaces=1,Minimum=.1m,Maximum=1000,Value=30};
 private readonly ComboBox delivery=new(){Name="DeliveryType",DropDownStyle=ComboBoxStyle.DropDownList};
 private readonly CheckBox late=new(){Text="Пізнє зв’язування через інтерфейс",Checked=true,AutoSize=true};
 private readonly Label result=new(){Name="Result",AutoSize=true,Padding=new Padding(0,15,0,0)};
 private readonly IDeliveryCostService earlyService=new EarlyDeliveryCostService();
 private readonly IDeliveryCostService lateService=new DeliveryCostService([new DepartmentDeliveryStrategy(),new CourierDeliveryStrategy(),new ExpressCourierDeliveryStrategy()]);
 public DeliveryForm()
 {
  Text="ПР-31–32 Розрахунок вартості доставки";ClientSize=new Size(640,570);MinimumSize=new Size(600,530);StartPosition=FormStartPosition.CenterScreen;Font=new Font("Segoe UI",10);
  var table=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2,Padding=new Padding(24),AutoScroll=true};table.ColumnStyles.Add(new(SizeType.Absolute,250));table.ColumnStyles.Add(new(SizeType.Percent,100));Controls.Add(table);
  int row=0;foreach(var(label,control)in new (string,Control)[]{("Фактична вага кг",weight),("Довжина см",length),("Ширина см",width),("Висота см",height),("Спосіб доставки",delivery)})
  {control.Dock=DockStyle.Fill;control.Margin=new Padding(6,8,6,8);table.Controls.Add(new Label{Text=label,AutoSize=true,Margin=new Padding(0,10,0,0)},0,row);table.Controls.Add(control,1,row++);}
  delivery.DataSource=new[]{"У відділення","Кур’єром додому","Експрес кур’єром додому"};
  table.Controls.Add(late,0,row++);table.SetColumnSpan(late,2);
  var calculate=new Button{Text="Розрахувати",Name="Calculate",AutoSize=true,Margin=new Padding(0,15,0,0)};table.Controls.Add(calculate,0,row++);table.Controls.Add(result,0,row);table.SetColumnSpan(result,2);calculate.Click+=(_,_)=>Calculate();AcceptButton=calculate;
 }
 private void Calculate()
 {
  var parcel=new VolumeWeight{Weight=weight.Value,Length=length.Value,Width=width.Value,Height=height.Value};var type=(DeliveryType)delivery.SelectedIndex;
  try
  {
   IDeliveryCostService service=late.Checked?lateService:earlyService;decimal price=service.Calculate(parcel,type);
   result.Text=$"Фактична вага: {parcel.Weight:F3} кг\nОб’ємна вага: {parcel.VolumetricWeight:F3} кг\nРозрахункова вага: {parcel.ChargeableWeight:F3} кг\nДоставка: {delivery.Text}\nВартість: {price:F2} грн";
  }
  catch(ArgumentException e){MessageBox.Show(this,e.Message,"Перевірте параметри");}
 }
}
