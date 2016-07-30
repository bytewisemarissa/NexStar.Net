namespace NexStar.NET.NexstarSupport.BaseCommandClasses
{
    internal interface ICommandResult<out T>
    {
        T TypedResult { get; }
    }
}
