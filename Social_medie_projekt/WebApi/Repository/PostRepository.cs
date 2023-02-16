﻿namespace WebApi.Repository
{
     public interface IPostRepository
    {
        Task<Posts> CreatePostAsync(Posts newPost);
        Task<Posts> DeletePostAsync(int id);
        Task<Posts> UpdatePostAsync(int id, Posts updatePost);
        Task<Posts> UpdatePostLikesAsync(int id, int like);
        Task<List<Posts>> GetAllAsync();
        Task<Posts?> GetPostByPostIdAsync(int PostId);
        Task<List<Posts?>> GetPostByUserIdAsync(int UserId);
        Task<Liked> CreateLikeAsync(Liked newLike);
        Task<Liked> DeleteLikeAsync(Liked like);
    }

    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;

        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Posts> CreatePostAsync(Posts newPost)
        {
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            return newPost;
        }

        public async Task<Posts> DeletePostAsync(int id)
        {
            var post = await GetPostByPostIdAsync(id);

            if(post != null)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }

        public async Task<Posts> UpdatePostAsync(int id, Posts updatePost)
        {
            var post = await GetPostByPostIdAsync(id);

            if(post != null)
            {
                post.User.UserId = updatePost.User.UserId;
                post.Title = updatePost.Title;
                post.Desc = updatePost.Desc;
                post.Tags = updatePost.Tags;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<Posts> UpdatePostLikesAsync(int id, int like)
        {
            var post = await GetPostByPostIdAsync(id);

            if (post != null)
            {
                post.Likes += like;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<List<Posts>> GetAllAsync()
        {
           return await _context.Posts.Include(c => c.User)
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        }

        public async Task<Posts?> GetPostByPostIdAsync(int postId)
        {
            return await _context.Posts.Include(c => c.User).FirstOrDefaultAsync(x => postId == x.PostId);
        }

        public async Task<List<Posts?>> GetPostByUserIdAsync(int userId)
        {
            return await _context.Posts.Include(c => c.User).Where(x => userId == x.UserId).ToListAsync();
        }

        public async Task<Liked> CreateLikeAsync(Liked newLike)
        {
            _context.Liked.Add(newLike);

            await _context.SaveChangesAsync();
            return newLike;
        }

        public async Task<Liked> DeleteLikeAsync(Liked like)
        {
            _context.Liked.Remove(like);

            await _context.SaveChangesAsync();
            return like;
        }
    }
}
