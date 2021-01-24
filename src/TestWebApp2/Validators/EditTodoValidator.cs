using FluentValidation;
using TestWebApp2.Contracts;

namespace TestWebApp2.Validators
{
    public class EditTodoValidator : AbstractValidator<EditToDoDto>
    {
        public EditTodoValidator()
        {
            RuleFor(x => x.Text).NotNull().NotEmpty();

            RuleFor(x => x.Text).MaximumLength(50);

            RuleFor(x => x.Priority).InclusiveBetween(1, 10);

            RuleFor(x => x.AssignedTo).SetValidator(new AssignerDtoValidator());

            RuleFor(x => x.Tags).Must(x => x.Length > 0 && x.Length < 4).When(x => x.Tags != null)
                .WithMessage("Tags should be greater than 0 and lesser than 4 or equals null");
        }
    }
}
