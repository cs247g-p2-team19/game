using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

/**
 * Locates and automatically assigns the first GameObject
 * in the scene with a component of the given type to this field.
 * 
 * Only works if the component inherits from AutoMonoBehaviour instead of MonoBehaviour.
 */
[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultWithTypeAttribute : Attribute, IAutoAttribute
{
    public Type Type { get; private set; }

    public AutoDefaultWithTypeAttribute(Type type) {
        Type = type;
    }

    public bool Apply(Component target, FieldInfo field) {
        if (field.FieldType.IsArray) {
            Type inner = field.FieldType.GetElementType();
            Object[] components = Object.FindObjectsOfType(inner);
            if (components.Length == 0) return false;

            field.SetValue(target, components);
            return true;
        }

        object found = Object.FindObjectOfType(Type);
        if (found.IsUnityNull()) return false;
        field.SetValue(target, found);
        return true;
    }
}