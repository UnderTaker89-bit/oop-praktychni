using System.Diagnostics;
using PostService.Client;
namespace PostService.WindowsApp;
internal static class Program
{
 [STAThread] static void Main()
 {
  ApplicationConfiguration.Initialize();Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
  Application.ThreadException+=(_,e)=>{ShowUnexpected(e.Exception);Application.Exit();};
  AppDomain.CurrentDomain.UnhandledException+=(_,e)=>{if(e.ExceptionObject is Exception error)ShowUnexpected(error);};
  using var http=new HttpClient{BaseAddress=new Uri(Environment.GetEnvironmentVariable("POSTSERVICE_URL")??"http://localhost:5211"),Timeout=TimeSpan.FromSeconds(10)};
  IPostServiceClient client=new PostServiceClient(http);Application.Run(new PostingList(client));
 }
 private static void ShowUnexpected(Exception exception)
 {
  Debug.WriteLine(ExceptionLog.Format(exception));
  MessageBox.Show("Сталася непередбачена помилка. Програму буде закрито. Зверніться до розробника.","Помилка застосунку",MessageBoxButtons.OK,MessageBoxIcon.Error);
 }
}
