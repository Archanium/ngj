using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Field)]
public class CameraXOffsetAttribute : Attribute
{
    public int Offset
    {
        get;
        private set;
    }

    public CameraXOffsetAttribute(int offset)
    {
        this.Offset = offset;
    }

    public static int GetOffset(SceneNames value)
    {
        var type = value.GetType();
        var field = type.GetField(value.ToString(), BindingFlags.Static | BindingFlags.Public);
        var attributes = field.GetCustomAttributes(typeof(CameraXOffsetAttribute), false);
        var attribute = attributes[0] as CameraXOffsetAttribute;
        var offset = attribute.Offset;
        return offset;
    }
}
