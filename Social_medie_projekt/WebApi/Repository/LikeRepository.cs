﻿namespace WebApi.Repository
{
    public interface ILikeRepository
    {
        Task<Like?> FindLike(int KeyId);
        Task<Like?> CheckLike(int userId, int postId);
        Task<List<Like>?> GetAllLikesFromUser(int userId);
        Task<Like> CreateLikeAsync(Like newLike);
        Task<Like> DeleteLikeAsync(int keyId);
    }

    public class LikeRepository : ILikeRepository
    {
        private readonly DatabaseContext _context;

        public LikeRepository(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<Like?> FindLike(int KeyId)
        {
            return await _context.Like.FirstOrDefaultAsync(x => KeyId == x.KeyId);
        }

        public async Task<Like?> CheckLike(int userId, int postId)
        {
            // skal KUN returnere noget hvis user har liket post
            return await _context.Like.Where(x => userId == x.UserId).Where(y => postId == y.PostId).FirstOrDefaultAsync();
        }


        public async Task<List<Like>?> GetAllLikesFromUser(int userId)
        {
            return await _context.Like.Include(c => c.User).Where(x => userId == x.UserId).ToListAsync();
        }

        public async Task<Like> CreateLikeAsync(Like like)
        {

            if (CheckLike(like.UserId, like.PostId) != null)
            {
                throw new Exception("Post aldready liked");
            }

            _context.Like.Add(like);
            await _context.SaveChangesAsync();
            return like;
        }

        public async Task<Like> DeleteLikeAsync(int keyId)
        {
            var like = await FindLike(keyId);

            if (like != null)
            {
                _context.Remove(like);
                await _context.SaveChangesAsync();
            }

            return like;
        }
    }
}
