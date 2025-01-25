using FluentValidation;
using FluentValidation.Results;

namespace InvoiceArchive.Infrastructure.FluentValidation
{
    public abstract class CustomAbstractValidator<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            ValidationResult validationResult = base.Validate(context);

            if (!validationResult.IsValid)
            {
                throw new CustomValidationException(validationResult.Errors);
            }

            return validationResult;
        }
    }
}
