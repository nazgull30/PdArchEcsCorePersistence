namespace PdArchEcsCorePersistence;

using VContainer.Pools.Impls;

public class StatePool<TState, TAccess> : SimpleMemoryPool<TState>
        where TState : TAccess, new()
        where TAccess : IProperty
{
    private readonly IObjectAccess<TAccess> _stateReset;

    public StatePool(IObjectAccess<TAccess> stateReset)
    {
        _stateReset = stateReset;
    }

    protected override void OnDespawned(TState item)
    {
        _stateReset.Reset(item);
    }
}

