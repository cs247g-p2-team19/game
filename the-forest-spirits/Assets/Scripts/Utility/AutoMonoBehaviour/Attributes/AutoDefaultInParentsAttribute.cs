using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

/**
 * Locates and automatically assigns a component on the current or any parent GameObject to the field.
 * Only works if the component inherits from AutoMonoBehaviour instead of MonoBehaviour.
 */
[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultInParentsAttribute : Attribute, IAutoAttribute
{
    public bool Apply(Component target, FieldInfo field) {
        if (field.FieldType.IsArray) {
            Component[] components = target.GetComponents(field.FieldType.GetElementType());
            if (components.Length == 0) return false;
            
            field.SetValue(target, components);
            return true;
        }
        
        object component = target.GetComponentInParent(field.FieldType);
        if (component.IsUnityNull()) return false;
        field.SetValue(target, component);
        return true;
    }
}