namespace PdArchEcsCorePersistence;

using System;

[AttributeUsage(AttributeTargets.Struct)]
public class PersistenceAttribute : Attribute
{
    public readonly int Priority;

    public PersistenceAttribute(int priority)
    {
        Priority = priority;
    }
}
