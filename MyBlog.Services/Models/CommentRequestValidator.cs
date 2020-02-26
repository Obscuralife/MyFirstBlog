using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Models
{
    public class CommentRequestValidator: AbstractValidator<CommentRequest>
    {
        public CommentRequestValidator()
        {
            RuleFor(r => r.Body).MaximumLength(200);
        }
    }
}
