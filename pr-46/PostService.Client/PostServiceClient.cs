using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using PostService.Dtos;
namespace PostService.Client;
public class PostingValidationException:Exception
{
 public Dictionary<string,string[]> Errors{get;}
 public PostingValidationException(Dictionary<string,string[]> errors):base("Сервер відхилив дані відправлення."){Errors=errors;}
}
internal class ValidationErrorResponse{public Dictionary<string,string[]> Errors{get;set;}=new();}
public interface IPostServiceClient
{
 Task<List<PostingGetDto>> GetPostingsAsync(CancellationToken token=default);
 Task<PostingGetDto> GetPostingAsync(int id,CancellationToken token=default);
 Task<PostingGetDto> CreatePostingAsync(PostingWriteDto dto,CancellationToken token=default);
 Task UpdatePostingAsync(int id,PostingWriteDto dto,CancellationToken token=default);
 Task DeletePostingAsync(int id,CancellationToken token=default);
}
public class PostServiceClient(HttpClient http):IPostServiceClient
{
 private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token)
 {
  try{return await http.SendAsync(request,token);}
  catch(TaskCanceledException e) when(!token.IsCancellationRequested){throw new HttpRequestException("Сервер не відповів за відведений час.",e);}
 }
 private static async Task EnsureSuccessAsync(HttpResponseMessage response,CancellationToken token)
 {
  if(response.IsSuccessStatusCode)return;
  if(response.StatusCode==HttpStatusCode.BadRequest)
  {
   ValidationErrorResponse? problem;
   try{problem=await response.Content.ReadFromJsonAsync<ValidationErrorResponse>(cancellationToken:token);}
   catch(JsonException e){throw new HttpRequestException("Сервер повернув некоректний опис помилки.",e,response.StatusCode);}
   var errors=problem?.Errors??new();if(errors.Count==0)errors[""]=["Сервер відхилив дані відправлення."];
   throw new PostingValidationException(errors);
  }
  throw new HttpRequestException($"Сервер повернув помилку {(int)response.StatusCode}.",null,response.StatusCode);
 }
 private async Task<T> ReadAsync<T>(HttpMethod method,string uri,PostingWriteDto? dto,CancellationToken token)
 {
  using var request=new HttpRequestMessage(method,uri);if(dto is not null)request.Content=JsonContent.Create(dto);
  using var response=await SendAsync(request,token);await EnsureSuccessAsync(response,token);
  return await response.Content.ReadFromJsonAsync<T>(cancellationToken:token)??throw new JsonException("Порожня відповідь сервера.");
 }
 private async Task WriteAsync(HttpMethod method,string uri,PostingWriteDto? dto,CancellationToken token)
 {
  using var request=new HttpRequestMessage(method,uri);if(dto is not null)request.Content=JsonContent.Create(dto);
  using var response=await SendAsync(request,token);await EnsureSuccessAsync(response,token);
 }
 public Task<List<PostingGetDto>> GetPostingsAsync(CancellationToken token=default)=>ReadAsync<List<PostingGetDto>>(HttpMethod.Get,"/postings",null,token);
 public Task<PostingGetDto> GetPostingAsync(int id,CancellationToken token=default)=>ReadAsync<PostingGetDto>(HttpMethod.Get,$"/postings/{id}",null,token);
 public Task<PostingGetDto> CreatePostingAsync(PostingWriteDto dto,CancellationToken token=default)=>ReadAsync<PostingGetDto>(HttpMethod.Post,"/postings",dto,token);
 public Task UpdatePostingAsync(int id,PostingWriteDto dto,CancellationToken token=default)=>WriteAsync(HttpMethod.Put,$"/postings/{id}",dto,token);
 public Task DeletePostingAsync(int id,CancellationToken token=default)=>WriteAsync(HttpMethod.Delete,$"/postings/{id}",null,token);
}
