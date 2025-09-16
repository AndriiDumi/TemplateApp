using FluentValidation;
using TemplateApp.Application.Dto;

namespace TemplateApp.Application.Validation;

public class TemplateValidator : AbstractValidator<TemplateRequest>
{
    public TemplateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(255)
            .WithMessage("Name must be less than or equal to 255 characters.");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(10000)
            .WithMessage("Content must be less than or equal to 10000 characters.");
    }
}
