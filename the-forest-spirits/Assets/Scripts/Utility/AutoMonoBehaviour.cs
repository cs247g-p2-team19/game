using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AutoMonoBehaviour : MonoBehaviour
{
    protected virtual void OnValidate() {
        foreach (var field in GetType().GetFields()) {
            object value = field.GetValue(this);
            if (!value.IsUnityNull()) continue;

            foreach (object attribute in field.GetCustomAttributes(true)) {
                if (attribute is AutoDefaultAttribute) {
                    object component = GetComponent(field.FieldType);
                    if (component.IsUnityNull()) continue;
                    field.SetValue(this, component);
                    break;
                }

                if (attribute is AutoDefaultTagAttribute tagAttr) {
                    object found = GameObject.FindGameObjectWithTag(tagAttr.Tag).GetComponent(field.FieldType);
                    if (found.IsUnityNull()) continue;
                    
                    field.SetValue(this, found);
                    break;
                }

                if (attribute is AutoDefaultWithTypeAttribute typeAttr) {
                    object found = FindObjectOfType(typeAttr.Type);
                    if (found.IsUnityNull()) continue;
                    field.SetValue(this, found);
                    break;
                }

                if (attribute is AutoDefaultMainCameraAttribute) {
                    if (field.FieldType != typeof(Camera)) continue;
                    field.SetValue(this, Camera.main);
                    break;
                }

                if (attribute is AutoDefaultInParentsAttribute) {
                    object component = GetComponentInParent(field.FieldType);
                    if (component.IsUnityNull()) continue;
                    field.SetValue(this, component);
                    break;
                }

                if (attribute is AutoDefaultInChildrenAttribute) {
                    object component = GetComponentInChildren(field.FieldType);
                    if (component.IsUnityNull()) continue;
                    field.SetValue(this, component);
                    break;
                }
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultMainCameraAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultTagAttribute : Attribute
{
    public string Tag { get; private set; }
    public AutoDefaultTagAttribute(string tag) {
        Tag = tag;
    }
}
[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultWithTypeAttribute : Attribute
{
    public Type Type { get; private set; }
    public AutoDefaultWithTypeAttribute(Type type) {
        Type = type;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultInParentsAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultInChildrenAttribute : Attribute
{
}