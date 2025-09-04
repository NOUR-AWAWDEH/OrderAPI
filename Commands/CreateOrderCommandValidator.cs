
using FluentValidation;

namespace OrdersAPI.Commands;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First Name should not be empty.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last Name should not be empty.");

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Order must contain a status.");

        RuleFor(x => x.TotalCost)
            .GreaterThan(0)
            .WithMessage("Total Cost must be greater than zero.");

    }
} 