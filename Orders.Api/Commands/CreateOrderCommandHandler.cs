using Ardalis.GuardClauses;
using Commons;
using OrdersService.Data;

namespace OrdersService.Commands;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrdersRepository ordersRepository, ILogger<CreateOrderCommandHandler> logger)
    {
        _ordersRepository = ordersRepository;
        _logger = logger;
    }
    public async Task HandleAsync(CreateOrderCommand command)
    {
        Guard.Against.Null(command);
        command.Order.Id = Guid.NewGuid();

        await _ordersRepository.CreateOrderAsync(command.Order);
        _logger.LogInformation($"inserted order with id: {command.Order.Id} into db");
    }
}