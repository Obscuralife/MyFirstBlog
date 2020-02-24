using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccessLayer.Models;
using MyBlog.Services;
using MyBlog.WebApi.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/comments")]
    [ApiController]
    [CustomExceptionFilter]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService service;

        public CommentController(ICommentService service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            this.service = service;
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a list of comments.", Type = typeof(Comment[]))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            var comments = await service.GetCommentsAsync();
            return Ok(comments);
        }

        [HttpGet]
        [Route("userId/{userId}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a comments by user id.", Type = typeof(Comment[]))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByUserId([FromRoute] string userId)
        {
            var comments = await service.GetUserCommentsAsync(userId);
            return Ok(comments);
        }

        [HttpGet]
        [Route("id/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a comment.", Type = typeof(Comment[]))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Comment>> GetComment([FromRoute] string id)
        {
            var comment = await service.GetCommentAsync(id);
            return Ok(comment);
        }


    }
}
