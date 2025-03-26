namespace PdArchEcsCorePersistence;

using Arch.Core;
using PdArchEcsCore.Worlds;

public abstract class ASingleEntityAccess<TWorld, TState, TAccess>
        : AEntityAccess<TState, TAccess>
        where TWorld : IWorld
        where TState : TAccess, new()
        where TAccess : IProperty
{
    private readonly IWorld _world;

    protected ASingleEntityAccess(
        TWorld world,
        IObjectAccess<TAccess> objectAccess
    ) : base(objectAccess)
    {
        _world = world;
    }

    protected override bool Skip(Entity entity) => false;

    public override void ReadState(TState state)
    {
        var entity = _world.Create();
        _objectAccess.SetState(entity, state);
        OnEntitySetState(entity);
    }

    protected virtual void OnEntitySetState(Entity entity)
    {
    }

    public override void WriteState(TState state)
    {
        var entity = _world.GetSingle(QueryDescription);
        TAccess access = state;
        if (!Skip(entity))
            _objectAccess.GetState(entity, ref access);
    }
}
