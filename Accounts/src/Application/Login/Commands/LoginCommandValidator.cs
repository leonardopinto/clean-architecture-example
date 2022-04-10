using FluentValidation;

namespace Accounts.Application.Login.Commands;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(v => v.Password)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .NotEmpty();
    }
}
