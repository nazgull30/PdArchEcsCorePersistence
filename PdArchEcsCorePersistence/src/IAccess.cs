namespace PdArchEcsCorePersistence;

using Arch.Core;

public interface IProperty
{
}

public interface IAccess<TState>
{
    public void ReadState(TState state);
    public void WriteState(TState state);
}

public interface IObjectAccess<TState> : IStateReset<TState>
{
    public void SetState(Entity entity, TState state);
    public void GetState(Entity entity, ref TState state);
}

public interface IStateReset<in TState>
{
    public void Reset(TState state);
}

public interface IPropertyAccess<in TProperty>
{
    public void SetObjectValue(Entity entity, TProperty property);
    public void SetPropertyValue(Entity entity, TProperty property);
    public void Reset(TProperty property);
}
