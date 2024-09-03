using FluentValidation;

namespace Book.API.Application.Books.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("The title cannot be empty.")
            .Length(1, 100).WithMessage("The title must be between 1 and 100 characters.");

        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("The description cannot be empty.")
            .Length(1, 500).WithMessage("The description must be between 1 and 500 characters.");

        RuleFor(command => command.Edition)
            .NotNull().WithMessage("The edition cannot be null.");

        RuleFor(command => command.ISBN)
            .NotEmpty().WithMessage("The ISBN cannot be empty.")
            .Length(10, 13).WithMessage("The ISBN must be between 10 and 13 characters.")
            .Matches(@"^\d{10}|\d{13}$").WithMessage("The ISBN must be a valid number of 10 or 13 digits.");

        RuleFor(command => command.Publisher)
            .NotEmpty().WithMessage("The publisher cannot be empty.")
            .Length(1, 100).WithMessage("The publisher must be between 1 and 100 characters.");

        RuleFor(command => command.PublisherYear)
            .InclusiveBetween((short)1900, (short)DateTime.Now.Year).WithMessage("The publisher year must be between 1900 and the current year.");

        RuleFor(command => command.PageAmount)
            .GreaterThan(0).WithMessage("The page amount must be greater than zero.");

        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage("The UserId cannot be null or empty.");

        RuleFor(command => command.AuthorId)
            .NotEmpty().WithMessage("The AuthorId cannot be null or empty.");

        RuleFor(command => command.GenreId)
            .NotEmpty().WithMessage("The GenreId cannot be null or empty.");
    }
}