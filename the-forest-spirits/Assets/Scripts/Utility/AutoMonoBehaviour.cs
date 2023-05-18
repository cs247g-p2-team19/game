using System;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class AutoMonoBehaviour : MonoBehaviour
{
    protected virtual void OnValidate() {
        foreach (var field in GetType().GetFields()) {
            object value = field.GetValue(this);
            if (!value.IsUnityNull()) continue;

            var attributes = field.GetCustomAttributes(true);
            bool applied =
                attributes.Any(attr => attr is IAutoAttribute auto && auto.Apply(this, field));
            bool required = attributes.Any(attr => attr is RequiredAttribute);

            if (!applied && required && isActiveAndEnabled) {
                Debug.LogWarning(
                    $@"<b><color=""red"">Warning!</color></b> Field <b>{field.Name} ({field.FieldType.Name})</b> in {name}'s {GetType().Name} component is marked as required, but is missing!",
                    this);
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class RequiredAttribute : Attribute
{ }

public interface IAutoAttribute
{
    public bool Apply(Component target, FieldInfo field);
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultAttribute : Attribute, IAutoAttribute
{
    public bool Apply(Component target, FieldInfo field) {
        object component = target.GetComponent(field.FieldType);
        if (component.IsUnityNull()) return false;
        field.SetValue(target, component);
        return true;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultMainCameraAttribute : Attribute, IAutoAttribute
{
    public bool Apply(Component target, FieldInfo field) {
        if (field.FieldType != typeof(Camera)) return false;
        field.SetValue(target, Camera.main);
        return true;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultTagAttribute : Attribute, IAutoAttribute
{
    public string Tag { get; private set; }

    public AutoDefaultTagAttribute(string tag) {
        Tag = tag;
    }

    public bool Apply(Component target, FieldInfo field) {
        object found = GameObject.FindGameObjectWithTag(Tag).GetComponent(field.FieldType);
        if (found.IsUnityNull()) return false;

        field.SetValue(target, found);
        return true;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultWithTypeAttribute : Attribute, IAutoAttribute
{
    public Type Type { get; private set; }

    public AutoDefaultWithTypeAttribute(Type type) {
        Type = type;
    }

    public bool Apply(Component target, FieldInfo field) {
        object found = Object.FindObjectOfType(Type);
        if (found.IsUnityNull()) return false;
        field.SetValue(target, found);
        return true;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultInParentsAttribute : Attribute, IAutoAttribute
{
    public bool Apply(Component target, FieldInfo field) {
        object component = target.GetComponentInParent(field.FieldType);
        if (component.IsUnityNull()) return false;
        field.SetValue(target, component);
        return true;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultInChildrenAttribute : Attribute, IAutoAttribute
{
    public bool Apply(Component target, FieldInfo field) {
        object component = target.GetComponentInChildren(field.FieldType);
        if (component.IsUnityNull()) return false;
        field.SetValue(target, component);
        return true;
    }
}