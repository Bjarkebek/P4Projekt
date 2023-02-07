﻿namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                List<PostResponse> posts = await _postService.GetAllPostsAsync();

                if(posts.Count() == 0)
                {
                    return NoContent();

                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> GetPostByIdAsync([FromRoute] int postId)
        {
            try
            {
                var postResponse = await _postService.GetPostById(postId);

                if(postResponse == null)
                {
                    return NotFound();
                }
                return Ok(postResponse);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody] PostRequest newPost)
        {
            try
            {
                var postResponse = await _postService.CreatePostAsync(newPost);

                return Ok(postResponse);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut]
        [Route("{postId}")]
        public async Task<IActionResult> UpdatePostAsync([FromRoute] int postId, [FromBody] PostUpdateRequest updatedPost)
        {
            try
            {
                var postResponse = await _postService.UpdatePostAsync(postId, updatedPost);

                if (postResponse == null)
                {
                    return NotFound();
                }

                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{postId}")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] int postId)
        {
            try
            {
                var postResponse = await _postService.DeletePostAsync(postId);

                if (postResponse == null)
                {
                    return NotFound();
                }

                return Ok(postResponse);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("Like")]
        public async Task<IActionResult> CreateLikeAsync([FromBody] LikedRequest newLike)
        {
            try
            {
                var likedResponse = await _postService.CreateLikeAsync(newLike);

                return Ok(likedResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Like")]
        public async Task<IActionResult> DeleteLikeAsync([FromBody] LikedRequest deleteLike)
        {
            try
            {
                var likedResponse = await _postService.DeleteLikeAsync(deleteLike);

                if (likedResponse == null)
                {
                    return NotFound();
                }

                return Ok(likedResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
    
}
