namespace BlogApp.Models
{
    public class CommentsModel
    {
        public int comment_id { get; set; }
        public int post_id { get; set; }
        public int user_id { get; set; }
        public string content { get; set; }
        public long comment_epoch { get; set; }
        public int parent_comment_id { get; set; }
        public string user_name { get; set; }

        public override string ToString()
        {
            return $"comment_id: {comment_id}, post_id: {post_id}, user_id: {user_id}, content: {content}, comment_epoch: {comment_epoch}, parent_comment_id: {parent_comment_id}";
        }
    }
}
