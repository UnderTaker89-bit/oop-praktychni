using System.Net;
using PostService.Client;
using PostService.Dtos;
namespace PostService.WindowsApp;
public class PostingList:Form
{
 private readonly IPostServiceClient client;
 private readonly DataGridView grid=new(){Name="Postings",Dock=DockStyle.Fill,ReadOnly=true,AllowUserToAddRows=false,AllowUserToDeleteRows=false,SelectionMode=DataGridViewSelectionMode.FullRowSelect,MultiSelect=false,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.DisplayedCells};
 private readonly FlowLayoutPanel actions=new(){Dock=DockStyle.Top,Height=52,Padding=new Padding(8)};
 public PostingList(IPostServiceClient client)
 {
  this.client=client;Text="ООП ПР-46 Обробка помилок PostService";Width=1200;Height=620;StartPosition=FormStartPosition.CenterScreen;
  Controls.Add(grid);Controls.Add(actions);
  foreach(var(text,action)in new(string,Func<Task>)[]{("Оновити",ReloadAsync),("Нове відправлення",()=>EditAsync(true)),("Редагувати",()=>EditAsync(false)),("Видалити",DeleteAsync)})
  {var button=new Button{Text=text,AutoSize=true};actions.Controls.Add(button);button.Click+=async(_,_)=>await action();}
  Load+=async(_,_)=>await ReloadAsync();
 }
 private PostingGetDto? Selected=>grid.CurrentRow?.DataBoundItem as PostingGetDto;
 private bool Retry()=>MessageBox.Show(this,"Сервер недоступний. Повторити операцію?","Помилка з’єднання",MessageBoxButtons.RetryCancel,MessageBoxIcon.Warning)==DialogResult.Retry;
 private async Task ReloadAsync()
 {
  actions.Enabled=false;
  try{while(true){try{grid.DataSource=await client.GetPostingsAsync();return;}catch(HttpRequestException e) when(e.StatusCode is null){if(!Retry())return;}}}
  finally{actions.Enabled=true;}
 }
 private async Task EditAsync(bool create)
 {
  if(!create&&Selected is null){MessageBox.Show(this,"Оберіть відправлення");return;}
  PostingGetDto? posting=null;int? id=create?null:Selected!.Id;actions.Enabled=false;
  try
  {
   if(id is not null)
   {
    while(true)
    {
     try{posting=await client.GetPostingAsync(id.Value);break;}
     catch(HttpRequestException e) when(e.StatusCode==HttpStatusCode.NotFound){MessageBox.Show(this,"Відправлення вже відсутнє. Список буде оновлено.");await ReloadAsync();return;}
     catch(HttpRequestException e) when(e.StatusCode is null){if(!Retry())return;}
    }
   }
   using var edit=new PostingEdit(client,posting);
   var result=edit.ShowDialog(this);if(result is DialogResult.OK or DialogResult.Abort)await ReloadAsync();
  }
  finally{actions.Enabled=true;}
 }
 private async Task DeleteAsync()
 {
  if(Selected is not {} p){MessageBox.Show(this,"Оберіть відправлення");return;}
  if(MessageBox.Show(this,$"Видалити відправлення №{p.Id}?","Підтвердження",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;
  actions.Enabled=false;
  try
  {
   while(true)
   {
    try{await client.DeletePostingAsync(p.Id);await ReloadAsync();return;}
    catch(HttpRequestException e) when(e.StatusCode==HttpStatusCode.NotFound){MessageBox.Show(this,"Відправлення вже видалено. Список буде оновлено.");await ReloadAsync();return;}
    catch(HttpRequestException e) when(e.StatusCode is null){if(!Retry())return;}
   }
  }
  finally{actions.Enabled=true;}
 }
}
