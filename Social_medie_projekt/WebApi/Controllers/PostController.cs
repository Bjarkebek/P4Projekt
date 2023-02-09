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
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                List<PostResponse> posts = await _postService.GetAllPostsAsync();

                if (posts.Count() == 0)
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
        public async Task<IActionResult> GetPostByPostIdAsync([FromRoute] int postId)
        {
            try
            {
                var postResponse = await _postService.GetPostByPostIdAsync(postId);

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

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetPostByUserIdAsync([FromRoute] int userId)
        {
            try
            {
                var postResponse = await _postService.GetPostByUserIdAsync(userId);

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

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody] PostRequest newPost)
        {
            try
            {
                var postResponse = await _postService.CreatePostAsync(newPost);

                return Ok(postResponse);
            }
            catch (Exception ex)
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
            catch (Exception ex)
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

        [HttpGet]
        [Route("Tag")]
        public async Task<IActionResult> GetAllTagsAsync()
        {
            try
            {
                List<TagResponse> tags = await _postService.GetAllTagsAsync();

                if (tags.Count() == 0)
                {
                    return NoContent();

                }
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("Tag/{tagId}")]
        public async Task<IActionResult> GetTagByIdAsync([FromRoute] int tagId)
        {
            try
            {
                var tagResponse = await _postService.GetTagById(tagId);

                if (tagResponse == null)
                {
                    return NotFound();
                }
                return Ok(tagResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("Tag")]
        public async Task<IActionResult> CreateTagAsync([FromBody] TagRequest newTag)
        {
            try
            {
                TagResponse tagResponse = await _postService.CreateTagAsync(newTag);

                return Ok(tagResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
    
}
