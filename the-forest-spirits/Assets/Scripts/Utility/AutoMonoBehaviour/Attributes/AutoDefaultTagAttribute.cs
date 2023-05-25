using System;
using System.Reflection;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/**
 * Locates and automatically assigns the first GameObject in the scene with the given tag to this field.
 * Only works if the component inherits from AutoMonoBehaviour instead of MonoBehaviour.
 */
[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultTagAttribute : Attribute, IAutoAttribute
{
    public string Tag { get; private set; }

    public AutoDefaultTagAttribute(string tag) {
        Tag = tag;
    }

    public bool Apply(Component target, FieldInfo field) {
        if (field.FieldType.IsArray) {
            Type inner = field.FieldType.GetElementType();
            Component[] components = GameObject.FindGameObjectsWithTag(Tag).SelectMany((go) => go.GetComponents(inner))
                .ToArray();
            if (components.Length == 0) return false;

            field.SetValue(target, components);
            return true;
        }

        object found = GameObject.FindGameObjectWithTag(Tag).GetComponent(field.FieldType);
        if (found.IsUnityNull()) return false;

        field.SetValue(target, found);
        return true;
    }
}