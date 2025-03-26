namespace PdArchEcsCorePersistence;

using Arch.Core;

public abstract class AEntityAccess<TState, TAccess> : IAccess<TState>
        where TAccess : IProperty
{
    protected readonly IObjectAccess<TAccess> _objectAccess;

    protected AEntityAccess(IObjectAccess<TAccess> objectAccess)
    {
        _objectAccess = objectAccess;
    }

    protected abstract QueryDescription QueryDescription { get; }

    protected abstract bool Skip(Entity entity);

    public abstract void ReadState(TState state);

    public abstract void WriteState(TState state);
}
