namespace PdArchEcsCorePersistence;

using System;

[AttributeUsage(AttributeTargets.Struct)]
public class EntityDescriptionAttribute : Attribute
{
    public string[] Names;

    public EntityDescriptionAttribute(params string[] names)
    {
        Names = names;
    }
}
