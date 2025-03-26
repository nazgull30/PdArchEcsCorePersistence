namespace PdArchEcsCorePersistence;

using System;
using System.Collections.Generic;
using Arch.Core;
using Godot;
using VContainer;

public abstract class ObjectPropertyAccess<TStateInterface> : IObjectAccess<TStateInterface>, IInitializable
    where TStateInterface : IProperty
{
    private readonly List<IPropertyAccess<TStateInterface>> _originators = [];

    public abstract void Initialize();

    public ObjectPropertyAccess<TStateInterface> AddOriginator<TPropertyAccess>()
        where TPropertyAccess : IPropertyAccess<TStateInterface>, new()
    {
        _originators.Add(new TPropertyAccess());
        return this;
    }

    public ObjectPropertyAccess<TStateInterface> AddOriginator(
        IPropertyAccess<TStateInterface> access
    )
    {
        _originators.Add(access);
        return this;
    }

    public void SetState(Entity entity, TStateInterface state)
    {
        for (var i = 0; i < _originators.Count; i++)
        {
            try
            {
                var originator = _originators[i];
                originator.SetObjectValue(entity, state);
            }
            catch (Exception e)
            {
                GD.PrintErr("obj: " + entity + ", state: " + state + ", e: " + e.Message);
                throw;
            }
        }
    }

    public void GetState(Entity entity, ref TStateInterface state)
    {
        for (var i = 0; i < _originators.Count; i++)
        {
            var originator = _originators[i];
            originator.SetPropertyValue(entity, state);
        }
    }

    public void Reset(TStateInterface state)
    {
        for (var i = 0; i < _originators.Count; i++)
        {
            var originator = _originators[i];
            originator.Reset(state);
        }
    }
}
