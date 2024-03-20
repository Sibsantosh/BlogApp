namespace BlogApp.Models
{
    public class Posts
    {
        public int Post_id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Author_id { get; set; }
        public long Publication_epoch { get; set; }
        public long Last_updated_epoch { get; set; }
        public int Status { get; set; }
        public byte[] Picture_data { get; set; }
        public BlogUsers Author { get; set; }
        public override string? ToString()
        {
            return $"post id = {Post_id} title = {Title} content = {Content} author id = {Author_id} publucation and lastupdated = {Publication_epoch},{Last_updated_epoch} status={Status} picture data ={Picture_data} author ={Author}";
        }

        // Navigation property to represent the relationship with BlogUser
        
    }
}
