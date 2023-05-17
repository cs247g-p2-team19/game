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
                if (attribute is not AutoDefaultAttribute attr) continue;

                if (attr.Tag != null) {
                    object found = GameObject.FindGameObjectWithTag(attr.Tag).GetComponent(field.FieldType);
                    if (!found.IsUnityNull()) {
                        field.SetValue(this, found);
                        break;
                    }
                }

                if (attr.Type != null) {
                    object found = FindObjectOfType(attr.Type);
                    if (!found.IsUnityNull()) {
                        field.SetValue(this, found);
                        break;
                    }
                }

                if (attr.MainCamera && field.FieldType == typeof(Camera)) {
                    field.SetValue(this, Camera.main);
                    break;
                }

                object component = GetComponent(field.FieldType);
                if (!component.IsUnityNull()) {
                    field.SetValue(this, component);
                }
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class AutoDefaultAttribute : Attribute
{
    public string Tag = null;
    public Type Type = null;
    public bool MainCamera = false;
}