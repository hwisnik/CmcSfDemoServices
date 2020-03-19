namespace Shared.Handlers
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
        void HandleLog(TCommand command, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0);
    }
}
