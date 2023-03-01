﻿namespace WebApi.Service
{
    public interface IPostService
    {
        // Post
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostByPostIdAsync(int postId);
        Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int userId);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse> UpdatePostAsync(int postId, PostUpdateRequest updatePost);
        Task<PostResponse?> DeletePostAsync(int postId);

        // Tag
        Task<List<TagResponse>> GetAllTagsAsync();
        Task<TagResponse?> GetTagById(int Id);
        Task<List<TagResponse>> GetTagsByPostIdAsync(int postId);
        Task<TagResponse> CreateTagAsync(TagRequest newTag);
        Task<TagResponse> UpdateTagAsync(TagRequest newTag);
        Task<List<PostTagResponse>> GetPostTagsByPostIdAsync(int postId);
        Task<PostTagResponse> CreatePostTagAsync(int postId, int tagId);
        Task<PostTagResponse> UpdatePostTagByPostIdAsync(int postId, int tagId);
        Task<PostTagResponse> DeletePostTagByPostIdAsync(int postId, int tagId);
        Task<List<TagResponse>> GetTagsByPostIdAsync(int postId);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }



        private static PostResponse MapPostToPostResponse(Post post, List<TagResponse> tag)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                Likes = post.Likes,
                //Tags = post.Tags.Select(x => new PostTagResponse
                //{
                //    PostId = x.PostId,
                //    TagId = x.TagId,
                //    Name = x.Name
                //}).ToList(),
                User = new PostUserResponse
                {
                    UserId = post.User.UserId,
                    UserName = post.User.UserName,
                }
            };
        }

        private Post MapPostRequestToPost(PostRequest postRequest)
        {
            return new Post
            {
                UserId = postRequest.UserId,
                Title = postRequest.Title,
                Desc = postRequest.Desc,
                //Tags = List<postRequest>.Tags,
            };
        }
         
        private Post MapPostUpdateRequestToPost(PostUpdateRequest postUpdateRequest)
        {
            return new Post
            {
                Title = postUpdateRequest.Title,
                Desc = postUpdateRequest.Desc,
            };
        }
              

        




        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            List<Post> posts = await _postRepository.GetAllAsync();

            if (posts == null)
            {
                throw new ArgumentNullException();
            }

            //Re-using CreateTagAsync since it already checks tag and does the necessary processes
            //var tags = updatePost.Tags.Select(tag => UpdateTagAsync(tag).Result).ToList();

            return posts.Select(post => MapPostToPostResponse(post, GetTagsByPostIdAsync(post.PostId).Result)).ToList();
        }

        public async Task<PostResponse?> GetPostByPostIdAsync(int postId)
        {
            var post = await _postRepository.GetPostByPostIdAsync(postId);

            if (post != null)
            {
                return MapPostToPostResponse(post, await GetTagsByPostIdAsync(postId));
            }

                return null;
        }        

        public async Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int userId)
        {
            List<Post?> post = await _postRepository.GetAllPostsByUserIdAsync(userId);

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            return post.Select(post => MapPostToPostResponse(post, GetTagsByPostIdAsync(post.PostId).Result)).ToList();
        }

        public async Task<PostResponse> CreatePostAsync(PostRequest newPost)
        {
            var post = await _postRepository.CreatePostAsync(MapPostRequestToPost(newPost));
            if (post == null)
            {
                throw new ArgumentNullException();
            }

            var tags = newPost.Tags.Select(tag => CreateTagAsync(tag).Result).ToList();

            _ = tags.Select(tagResponse => CreatePostTagAsync(post.PostId, tagResponse.TagId).Result).ToList();

            return MapPostToPostResponse(post, tags);

        }

            return MapPostToPostResponse(post);
        }

        public async Task<PostResponse> UpdatePostAsync(int postId, PostUpdateRequest updatePost)
        {
            var oldtags = await _postRepository.GetTagsByPostIdAsync(postId);

            if (oldtags == null)
            {
                throw new ArgumentNullException();
            }

            var updateTags = updatePost.Tags
                .Select(x => MapTagRequestToTag(x))
                .ToList();

            var post = await _postRepository.UpdatePostAsync(postId, MapPostUpdateRequestToPost(updatePost));
            var tags = updatePost.Tags.Select(tag => UpdateTagAsync(tag).Result).ToList();

            if (post != null)
            {
                throw new ArgumentNullException();
            }

            var tagCreate = updateTags
                .Where(x => !(oldtags.Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            var tagDelete = oldtags
                .Where(x => !(updateTags.Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            var tagsDeleted = tagDelete
                .Select(x => DeletePostTagByPostIdAsync(postId, x.TagId).Result)
                .ToList();

            var tagsCreated = tagCreate
                .Select(x => CreateTagAsync(x).Result)
                .ToList();

            _ = tagsCreated
                .Select(x => CreatePostTagAsync(post.PostId, x.TagId).Result)
                .ToList();

            var currentTags = await _postRepository.GetTagsByPostIdAsync(postId);

            if (currentTags == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostToPostResponse(post, currentTags);
        }

        public async Task<List<PostTagResponse>> GetPostTagsByPostIdAsync(int postId)
        {
            var postTags = await _postRepository.GetPostTagsByPostId(postId);

            if (postTags == null)
            {
                throw new ArgumentNullException();
            }

            return postTags.Select(posttag => MapPostTagToPostTagResponse(posttag)).ToList();
        }

        public async Task<PostResponse?> DeletePostAsync(int postId)
        {
            var post = await _postRepository.DeletePostAsync(postId);
            if (post == null)
            {
                return null;
            }

            var tags = post.Tags.Select(tag => GetTagsByPostIdAsync(postId).Result).ToList();
            return MapPostToPostResponse(post, null);
        }

        public async Task<PostTagResponse> UpdatePostTagByPostIdAsync(int postId, int tagId)
        {

            var postTag = await _postRepository.UpdatePostTagAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }


            return MapPostTagToPostTagResponse(postTag);
        }

        public async Task<PostTagResponse> DeletePostTagByPostIdAsync(int postId, int tagId)
        {
            var postTag = await _postRepository.DeletePostTagAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostTagToPostTagResponse(postTag);
        }


        // ---------------------- TAGS ----------------------
        public async Task<List<TagResponse>> GetAllTagsAsync()
        {
            List<Tag> tags = await _postRepository.GetAllTagsAsync();

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse?> GetTagById(int tagId)
        {
            var tags = await _postRepository.GetTagByIdAsync(tagId);

            if (tags == null)
            {
                return null;
            }
            return MapTagToTagResponse(tags);
        }

        public async Task<List<TagResponse>> GetTagsByPostIdAsync(int postId)
        {
            var tags = await _postRepository.GetTagsByPostIdAsync(postId);

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse> CreateTagAsync(TagRequest newTag)
        {
            var tag = await _postRepository.CreateTagAsync(MapTagRequestToTag(newTag));

            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }

        private async Task<Tag> CreateTagAsync(Tag newTag)
        {
            var tag = await _postRepository.CreateTagAsync(newTag);

            if (tag == null)
            {
                throw new ArgumentNullException();
            }

            return tag;
        }

        public async Task<TagResponse> UpdateTagAsync(TagRequest newTag)
        {
            var tag = await _postRepository.UpdateTagAsync(MapTagRequestToTag(newTag));

            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }



        public async Task<PostTagResponse> CreatePostTagAsync(int postId, int tagId)
        {
            var postTag = await _postRepository.CreatePostTagAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostTagToPostTagResponse(postTag);
        }





        // ----------------------------------------------
        // ------------- Like RES/REQ -------------------
        // ----------------------------------------------

        //private static Like MapLikeRequestToLike(LikeRequest likedRequest)
        //{
        //    return new Like
        //    {
        //        UserId = likedRequest.UserId,
        //        PostId = likedRequest.PostId,
        //    };
        //}

        private static PostResponse MapPostToPostResponse(Posts post, List<Tag> tags)
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
                    UserName = post.PostUser.UserName,
                },
                Tags = tags.Select(x => new TagResponse
                {
                    TagId = x.TagId,
                    Name = x.Name,
                })
                .ToList()
            };
        }

        private static PostResponse MapPostToPostResponse(Posts post)
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
                    UserName = post.PostUser.UserName,
                },//Hvor kommer tags fra
                //Tags = post.Tags.Select(x => new PostTagResponse
                //{
                //    TagId = x.TagId,
                //    Name = x.Name
                //}).ToList()
            };
        }
        //private static LikeResponse MapLikeToLikeResponse(Like like)
        //{
        //    return new LikeResponse
        //    {
        //        UserId = like.UserId,
        //        PostId = like.PostId,
        //    };
        //}






        // ----------------------------------------------
        // ------------- TAG RES/REQ --------------------
        // ----------------------------------------------


        private static TagResponse MapTagToTagResponse(Tag tag)
        {
            return new TagResponse
            {
                TagId = tag.TagId,
                Name = tag.Name,
            };
        }


        private static Tag MapTagRequestToTag(TagRequest tagRequest)
        {
            return new Tag
            {
                Name = tagRequest.Name
            };
        }


        private PostTagResponse MapPostTagToPostTagResponse(PostTag postsTag)
        {
            return new PostTagResponse
            {
                PostId = postsTag.PostId,
                TagId = postsTag.TagId,
            };
        }
        //New mapping
        private static PostsTag MapPostTagResponseToPostTag(PostTagResponse postTagResponse)
        {
            return new PostsTag
            {
                PostId = postTagResponse.PostId,
                TagId = postTagResponse.TagId,
            };
        }
        // change name
        private static PostsTag MapPostTagRequestToPostTag(int postId, int tagId)
        {
            return new PostTag
            {
                PostId = postId,
                TagId = tagId,
            };
        }

        private static TagResponse MapTagToTagResponse(Tag tag)
        {
            return new TagResponse
            {
                TagId = tag.TagId,
                Name = tag.Name,
            };
        }

        private static Tag MapTagRequestToTag(TagRequest tagRequest)
        {
            return new Tag
            {
                Name = tagRequest.Name
            };
        }
    }
}
