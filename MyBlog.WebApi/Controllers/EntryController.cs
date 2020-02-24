using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccessLayer.Models;
using MyBlog.Services;
using MyBlog.Services.Models;
using MyBlog.WebApi.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace MyBlog.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/entries")]
    [ApiController]
    [CustomExceptionFilter]
    public class EntryController : ControllerBase
    {
        private readonly IEntryService service;

        public EntryController(IEntryService service)
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a list of entries.", Type = typeof(Entry[]))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            var entries = await service.GetEntriesAsync();
            return Ok(entries);
        }

        [HttpGet]
        [Route("category/{category}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a list of category entries.", Type = typeof(Entry[]))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries(string category)
        {
            var entries = await service.GetEntriesAsync(category);
            return Ok(entries);
        }

        [HttpGet]
        [Route("article/{article}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a entry by article.", Type = typeof(Entry))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Entry>> GetEntryByArticle(string article)
        {
            var entry = await service.GetEntryAsync(article);
            return Ok(entry);
        }
        [HttpGet]
        [Route("{entryId}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns a entry by id.", Type = typeof(Entry))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Entry>> GetEntryById(string entryId)
        {
            var entry = await service.GetEntryByIdAsync(entryId);
            return Ok(entry);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.Created, Description = "Creates a new entry.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Description = "Entry with this article already existed.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddEntry([FromBody] EntryRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = await service.CreateEntryAsync(createRequest);
            var location = string.Format("/api/entries/{0}", entry.Id);
            return Created(location, entry);
        }

        [HttpDelete]
        [Route("{entryId}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Deletes an existed entry.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteEntryAsync([FromRoute] string entryId)
        {
            await service.RemoveEntryAsync(entryId);
            return NoContent();
        }

        [HttpPut]
        [Route("{entryId}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Updates an existed entry.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateEntryAsync([FromRoute] string entryId, [FromBody] UpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await service.UpdateEntryAsync(entryId, request);
            return NoContent();
        }

        [HttpPost]
        [Route("{entryId}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Add comment to entry.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddCommentAsync([FromRoute] string entryId, [FromBody] CommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await service.AddCommentAsync(entryId, request);
            return Ok(comment);
        }

    }
}
