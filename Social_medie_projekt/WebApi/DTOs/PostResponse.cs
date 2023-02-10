﻿namespace WebApi.DTOs
{
    public class PostResponse
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Desc { get; set; }

        public DateTime Date { get; set; } = DateTime.Now.Date;

        public int? Likes { get; set; }

        public PostUserResponse? User { get; set; } = new();

        //public List<PostTagResponse> Tags { get; set; } = new();
    }
    public class PostUserResponse
    {
        public string UserName { get; set; } = string.Empty;
    }
    //public class PostTagResponse
    //{
    //    public int TagId { get; set; }

    //    public string Name { get; set; }
    //}
}
