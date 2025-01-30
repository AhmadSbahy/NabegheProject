using FluentValidation.Results;

namespace Nabeghe.Application.Exceptions
{
	public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationException(ValidationResult validation)
        {
            foreach (var validationFailure in validation.Errors)
            {
                Errors.Add(validationFailure.ErrorMessage);
            }
        }
    }
}
