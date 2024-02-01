using Ardalis.GuardClauses;
using Commons;
using Payments.Api.Enums;
using Payments.Api.Interfaces;

namespace Payments.Api.Commands;

public class ProcessPaymentCommandHandler : ICommandHandler<ProcessPaymentCommand>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly ILogger<ProcessPaymentCommandHandler> _logger;

    public ProcessPaymentCommandHandler(IPaymentRepository paymentRepository,
        IPaymentProcessor paymentProcessor,
        ILogger<ProcessPaymentCommandHandler> logger)
    {
        _paymentRepository = paymentRepository;
        _paymentProcessor = paymentProcessor;
        _logger = logger;
    }

    public async Task HandleAsync(ProcessPaymentCommand command)
    {
        Guard.Against.Null(command);

        try
        {
            command.PaymentDetails.Status = PaymentStatus.UnPaid;

            var paymentProcessingResult = await _paymentProcessor.ProcessPaymentAsync(command.PaymentDetails);
            if (!paymentProcessingResult) throw new ApplicationException();

            command.PaymentDetails.Status = PaymentStatus.Paid;

            await _paymentRepository.CreatePaymentDetailsAsync(command.PaymentDetails);
            _logger.LogInformation($"inserted payment with id: {command.PaymentDetails.Id} into db");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error has occurred while processing payment");
        }
    }
}