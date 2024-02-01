using Ardalis.GuardClauses;
using Commons;
using Shipping.Api.Interfaces;

namespace Shipping.Api.Commands;

public class CreateShippingRequestCommandHandler : ICommandHandler<CreateShippingRequestCommand>
{
    private readonly IShippingRequestRepository _repository;
    private readonly ILogger<CreateShippingRequestCommandHandler> _logger;

    public CreateShippingRequestCommandHandler(
        IShippingRequestRepository repository,
        ILogger<CreateShippingRequestCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task HandleAsync(CreateShippingRequestCommand command)
    {
        Guard.Against.Null(command);

        try
        {
            await _repository.CreateShippingRequestAsync(command.ShippingRequest);
            _logger.LogInformation($"inserted shipping request with id: {command.ShippingRequest.Id} into db");
        }
        catch
        {
           
        }
    }
}