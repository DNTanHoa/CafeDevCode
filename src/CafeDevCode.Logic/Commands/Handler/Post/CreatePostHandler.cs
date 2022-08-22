using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Commands.Handler
{
    public class CreatePostHandler 
        : IRequestHandler<CreatePost, BaseCommandResultWithData<Post>>
    {
        private readonly IMapper mapper;
        private readonly AppDatabase database;

        public CreatePostHandler(IMapper mapper,
            AppDatabase database)
        {
            this.mapper = mapper;
            this.database = database;
        }

        public Task<BaseCommandResultWithData<Post>> Handle(CreatePost request,
            CancellationToken cancellationToken)
        {
            var result = new BaseCommandResultWithData<Post>();

            try
            {
                var post = mapper.Map<Post>(request);
                post.SetCreateInfo(request.UserName ?? string.Empty, AppGlobal.SysDateTime);

                database.Post.Add(post);
                database.SaveChanges();

                if(request.Tags != null)
                {
                    var postTags = request.Tags.Select(x => new PostTag()
                    {
                        PostId = post.Id,
                        TagId = x.Id,
                        CreatedAt = AppGlobal.SysDateTime,
                        CreatedBy = request.UserName,
                    });

                    database.PostTags.AddRange(postTags);
                }

                if(request.Categories != null)
                {
                    var postCategories = request.Categories.Select(x => new PostCategory()
                    {
                        PostId = post.Id,
                        CategoryId = x.Id,
                        CreatedAt = AppGlobal.SysDateTime,
                        CreatedBy = request.UserName,
                    });

                    database.PostCategories.AddRange(postCategories);
                }

                if(request.Relates != null)
                {
                    var postRelates = request.Relates.Select(x => new PostRelated()
                    {
                        PostId = post.Id,
                        RelatedId = x.Id,
                        CreatedAt = AppGlobal.SysDateTime,
                        CreatedBy = request.UserName,
                    });

                    database.PostRelateds.AddRange(postRelates);
                }

                database.SaveChanges();

                result.Data = post;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Messages = ex.Message;
            }

            return Task.FromResult(result);
        }
    }
}
