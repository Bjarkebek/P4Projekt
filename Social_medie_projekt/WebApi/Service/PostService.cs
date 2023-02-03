﻿namespace WebApi.Service
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostById(int Id);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse?> EditPostAsync(int postId, PostRequest updatePost);
        Task<PostResponse?> DeletePost(int Id);

    }
 
    public class PostService : IPostService
    {

        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        private PostResponse MapPostToPostResponse(Posts post)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                Likes = post.Likes,
                User = new PostUserResponse
                {
                    UserId = post.UserId,
                    FirstName = post.User.FirstName,
                    LastName = post.User.LastName,
                    Address = post.User.Address,
                    Created = post.User.Created,
                },
                Tags = post.Tags.Select(x => new PostTagResponse
                {
                    TagId = x.TagId,
                    tag = x.tag
                }).ToList()
            };
        }

        private Posts MapPostRequestToPost(PostRequest postRequest)
        {
            return new Posts
            {
                UserId = postRequest.UserId,
                Title = postRequest.Title,
                Desc = postRequest.Desc,
                Tags = postRequest.Tags.Select(x => new Tag 
                {
                    tag = x.Tag,
                }).ToList()
            };
        }

        public async Task<PostResponse> CreatePostAsync(PostRequest newPost)
        {
            //TagRepository tagRepository = new TagRepository(_context);
            //var taglist = await tagRepository.GetAllAsync();
            //for (int i = 0; i < taglist.Count; i++)
            //{
            //    if (taglist.IndexOf(newPost.Tags[i]) === -1)
            //    {

            //    }
            //}
            //foreach (string tag in newPost.Tags)
            //{
            //    string[] taglist = from tags in _c
            //}
            var post = await _postRepository.CreatePostAsync(MapPostRequestToPost(newPost));

            if(post == null)
            {
                throw new ArgumentNullException();
            }
            //if(newPost.Tags.IndexOf())

            return MapPostToPostResponse(post);

        }

        public async Task<PostResponse?> DeletePost(int Id)
        {
         
            var post = await _postRepository.DeletePostAsync(Id);

            if(post == null)
            {
                return null;
            }

            return MapPostToPostResponse(post);
        }

        public async Task<PostResponse?> EditPostAsync(int postId, PostRequest updatePost)
        {
            var post = await _postRepository.EditPost(postId, MapPostRequestToPost(updatePost));

            if(post != null)
            {
                return MapPostToPostResponse(post);
            }
            return null;
        }

        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            List<Posts> posts = await _postRepository.GetAllAsync();

            if(posts == null)
            {
                throw new ArgumentNullException();
            }

            return posts.Select(post => MapPostToPostResponse(post)).ToList();
        }

        public async Task<PostResponse?> GetPostById(int Id)
        {
           var posts = await _postRepository.GetPostByIdAsync(Id);

            if(posts == null)
            {
                return null;
            }
            return MapPostToPostResponse(posts);
        }
    }
}
