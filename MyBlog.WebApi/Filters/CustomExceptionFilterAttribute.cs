using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyBlog.Services.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public async override Task OnExceptionAsync(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case RequestedResourceNotFoundException _:
                    {
                        context.Result = new ContentResult() 
                        {
                            Content = $"this {context.Exception.Message} сan not be found.",
                            StatusCode = (int)HttpStatusCode.NotFound
                        };
                        break;
                    }

                case RequestedResourceHasConflictException _:
                    {
                        context.Result = new ContentResult()
                        {
                            Content = $"this {context.Exception.Message} already exists",
                            StatusCode = (int)HttpStatusCode.Conflict
                        };
                        break;
                    }

                case RequestedResourceHasBadRequest _:
                    {
                        context.Result = new ContentResult()
                        {
                            Content = $"{context.Exception.Message}",
                            StatusCode = (int)HttpStatusCode.BadRequest
                        };
                        break;
                    }

                case Exception _:
                    {
                        context.Result = new ContentResult() 
                        {
                            Content = $"An exception occured: {context.Exception.Message}\n" +
                            $"{context.Exception.StackTrace} in the method {context.Exception.Source}",
                            StatusCode = (int)HttpStatusCode.InternalServerError
                        };
                        break;
                    }
            }

            context.ExceptionHandled = true;
            await base.OnExceptionAsync(context);
        }
    }
}
