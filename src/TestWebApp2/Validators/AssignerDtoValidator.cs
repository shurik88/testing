using FluentValidation;
using TestWebApp2.Contracts;

namespace TestWebApp2.Validators
{
    public class AssignerDtoValidator : AbstractValidator<AssignerDto>
    {
        public AssignerDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();

            RuleFor(x => x.Name).MaximumLength(50);

            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
