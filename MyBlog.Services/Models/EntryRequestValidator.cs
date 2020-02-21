using FluentValidation;

namespace MyBlog.Services.Models
{
    public class EntryRequestValidator : AbstractValidator<EntryRequest>
    {
        public EntryRequestValidator()
        {
            RuleFor(r => r.Article).MaximumLength(2000);
            RuleFor(r => r.Body).MinimumLength(20);
            RuleFor(r => r.Category).MinimumLength(2);
        }
    }
}
