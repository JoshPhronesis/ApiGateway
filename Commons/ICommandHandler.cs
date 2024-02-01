namespace Commons;

public interface ICommandHandler<in TCommand> where TCommand: ICommand
{
    Task HandleAsync(TCommand command);
}