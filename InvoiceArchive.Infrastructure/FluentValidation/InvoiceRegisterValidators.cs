using FluentValidation;
using InvoiceArchive.Application.Commands.CreateInvoiceRegister;

namespace InvoiceArchive.Infrastructure.FluentValidation
{
    public class CreateInvoiceRegisterCommandRequestValidator : CustomAbstractValidator<CreateInvoiceRegisterCommandRequest>
    {
        public CreateInvoiceRegisterCommandRequestValidator()
        {
            RuleFor(invoice => invoice.InvoiceHeader.InvoiceId).NotEmpty().WithMessage("InvoiceId is empty.");
            RuleFor(invoice => invoice.InvoiceHeader.SenderTitle).NotEmpty().WithMessage("SenderTitle is empty.");
            RuleFor(invoice => invoice.InvoiceHeader.ReceiverTitle).NotEmpty().WithMessage("ReceiverTitle is empty.");
            RuleFor(invoice => invoice.InvoiceHeader.Date).NotEmpty().WithMessage("Date is empty.");
        }
    }
}
