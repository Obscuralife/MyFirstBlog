using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Models.Identity
{
    public class LogInRequestValidator : AbstractValidator<LogInRequest>
    {
        public LogInRequestValidator()
        {
            RuleFor(i => i.Email).EmailAddress();
        }
    }
}
