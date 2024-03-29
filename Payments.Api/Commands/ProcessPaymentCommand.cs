using Payments.Api.Entities;
using Commons;

namespace Payments.Api.Commands;

public class ProcessPaymentCommand : ICommand
{
    public PaymentDetail PaymentDetails { get; }
    public ProcessPaymentCommand(PaymentDetail paymentDetails)
    {
        PaymentDetails = paymentDetails;
    }
}