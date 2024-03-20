using Microsoft.Extensions.Hosting;

namespace BlogApp.Models
{
    public class FetchPostsResponseModel
    {
       public List<Posts> postsList  {  get; set; }
    }
}
