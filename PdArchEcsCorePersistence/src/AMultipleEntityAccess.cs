namespace PdArchEcsCorePersistence;

using System;
using System.Collections.Generic;
using Arch.Core;
using Godot;
using PdArchEcsCore.Utils;
using PdArchEcsCore.Worlds;
using PdPools;

public abstract class AMultipleEntityAccess<TWorld, TState, TStateInterface>
        : AEntityAccess<List<TState>, TStateInterface>
        where TWorld : IWorld
        where TState : TStateInterface, new()
        where TStateInterface : IProperty
{
    private readonly IWorld _world;

    protected AMultipleEntityAccess(
        TWorld world,
        IObjectAccess<TStateInterface> objectAccess
    ) : base(objectAccess)
    {
        _world = world;
    }

    protected override bool Skip(Entity entity) => false;

    public override void ReadState(List<TState> states)
    {
        using var _ = ListPool<Entity>.Get(out var buffer);
        var length = states.Count;
        if (buffer.Capacity < length)
            buffer.Capacity = length;

        for (var i = 0; i < length; i++)
        {
            var state = states[i];
            var entity = _world.Create();

            try
            {
                _objectAccess.SetState(entity, state);
            }
            catch (Exception e)
            {
                var exception =
                    new AggregateException(
                        $"[{GetType().Name}] Cannot set entity state {typeof(TState).Name} -> {state}", e);
                GD.PrintErr(exception);

                _world.Destroy(entity);
                continue;
            }

            buffer.Add(entity);
        }

        OnEntitySetState(buffer);
    }

    protected virtual void OnEntitySetState(List<Entity> entities)
    {
    }

    public override void WriteState(List<TState> states)
    {
        using var _ = ListPool<Entity>.Get(out var buffer);
        _world.GetEntities(QueryDescription, buffer);
        buffer.RemoveAllWithSwap(Skip);
        var count = buffer.Count;
        for (var i = 0; i < count; i++)
        {
            var entity = buffer[i];
            TStateInterface access = new TState();
            _objectAccess.GetState(entity, ref access);
            try
            {
                states.Add((TState)access);
            }
            catch (Exception)
            {
                GD.Print("access: " + access + ", states: " + states);
                throw;
            }
        }
    }
}
