using FluentValidation.Results;

namespace ChatApp.Application.Commons.Exceptions
{
    public class ValidatorException : Exception
    {
        public List<ValidationFailure> Errors { get; }

        public ValidatorException(List<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public IDictionary<string, string[]> ToDictionary()
        {
            return Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());
        }
    }
}
