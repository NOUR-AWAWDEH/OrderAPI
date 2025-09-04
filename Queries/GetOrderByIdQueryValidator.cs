using FluentValidation;

namespace OrdersAPI.Queries;

public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage("Order ID must be greater than zero.")
            .NotEmpty()
            .WithMessage("Order ID should not be empty.");
    }   
}