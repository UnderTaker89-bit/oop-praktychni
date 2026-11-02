using System.Reflection;
namespace PostService.Client;
public static class ExceptionLog
{
 public static Exception Unwrap(Exception exception)
 {while(exception is TargetInvocationException invocation && invocation.InnerException is Exception inner)exception=inner;return exception;}
 public static string Format(Exception exception)
 {var actual=Unwrap(exception);return $"{DateTimeOffset.Now:O} | {actual.GetType().FullName} | {actual.Message}{Environment.NewLine}{actual}";}
}
