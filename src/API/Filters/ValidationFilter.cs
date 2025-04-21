using ChatApp.Application.Commons.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatApp.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            System.Console.WriteLine("running");
            if (context.ActionArguments.Any())
            {
                foreach (var argument in context.ActionArguments)
                {
                    if (argument.Value is null)
                    {
                        continue;
                    }

                    var validatorType = typeof(IValidator<>).MakeGenericType(
                        argument.Value.GetType()
                    );

                    if (_serviceProvider.GetService(validatorType) is IValidator validator)
                    {
                        var validationContext = new ValidationContext<object>(argument.Value);
                        var validationResult = await validator.ValidateAsync(validationContext);

                        if (!validationResult.IsValid)
                        {
                            throw new ValidatorException(validationResult.Errors);
                        }
                    }
                }
            }

            await next();
            return;
        }
    }
}
