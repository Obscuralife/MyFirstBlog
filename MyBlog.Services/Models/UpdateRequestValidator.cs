using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Models
{
    public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
    {
        public UpdateRequestValidator()
        {
            RuleFor(i => i.NewContent).MinimumLength(20);
        }
    }
}
