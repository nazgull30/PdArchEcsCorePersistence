namespace PdArchEcsCorePersistence;

using System;

[AttributeUsage(AttributeTargets.Interface)]
public class EntityStateAttribute : Attribute
{
    public bool Multiple;

    public EntityStateAttribute(bool multiple = false)
    {
        Multiple = multiple;
    }
}

