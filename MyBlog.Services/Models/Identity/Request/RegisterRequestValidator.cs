using FluentValidation;
using System.Text.RegularExpressions;

namespace MyBlog.Services.Models.Identity
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(i => i.Email).EmailAddress();
            RuleFor(i => i.Name).MinimumLength(2);
            RuleFor(i => i.LastName).MinimumLength(2);
            RuleFor(i => i.Password).Must(j => Regex.IsMatch(j, @"^(?=.*?[0-9])[^\s!\\%?*]{6,12}$"));
        }
    }
}
