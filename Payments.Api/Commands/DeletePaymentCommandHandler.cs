using Ardalis.GuardClauses;
using Commons;
using Payments.Api.Interfaces;

namespace Payments.Api.Commands;

public class DeletePaymentCommandHandler : ICommandHandler<DeletePaymentCommand>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILogger<DeletePaymentCommandHandler> _logger;

    public DeletePaymentCommandHandler(
        IPaymentRepository paymentRepository,
        ILogger<DeletePaymentCommandHandler> logger)
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
    }
    public async Task HandleAsync(DeletePaymentCommand command)
    {
        Guard.Against.Null(command);

        await _paymentRepository.DeletePaymentDetailsAsync(command.PaymentId);
       
        _logger.LogInformation($"deleted payment with id: {command.PaymentId} from db");
    }
}