using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Provides a BUNCH of utility methods.
/// </summary>
public static class Utility
{
    /// Function type. Linearly interpolates a type T between two values, from and to, according to a percent (0 to 1), t.
    public delegate T LerpFn<T>(T from, T to, float t);

    /// Function type. Takes in a value of type T and does something with it.
    public delegate void LerpValueCallback<in T>(T currentValue);

    /// Function type. Just does something.
    public delegate void Thunk();

    /// <summary>
    ///     For convenience, defines the in-out-eased lerp function on floats
    /// </summary>
    public static LerpFn<float> EaseInOutF => EaseInOut<float>(Mathf.Lerp);

    /// <summary>
    ///     Coroutine that waits [seconds] seconds and then calls [fn].
    /// </summary>
    public static IEnumerator WaitThen(float seconds, Thunk fn)
    {
        yield return new WaitForSeconds(seconds);
        fn();
    }

    /// <summary>
    ///     Coroutine that waits for [first] to finish and then calls [fn]
    /// </summary>
    public static IEnumerator WaitThen(Coroutine first, Thunk fn)
    {
        yield return first;
        fn();
    }

    /// <summary>
    ///     Coroutine linearly interpolates a value T from [from] to [to] over [totalSeconds] seconds,
    ///     calling [lerp] to do that, and calling [callback] once per frame to apply that value.
    /// </summary>
    public static IEnumerator Lerp<T>(T from, T to, float totalSeconds, LerpFn<T> lerp,
        LerpValueCallback<T> callback)
    {
        var currTime = 0f;
        while (currTime < totalSeconds)
        {
            currTime += Time.deltaTime;

            var value = lerp(from, to, currTime / totalSeconds);
            callback(value);
            yield return null;
        }
    }

    /// <summary>
    ///     Wraps a linear-interpolation function so that it eases in instead of linearly interpolates.
    /// </summary>
    public static LerpFn<T> EaseIn<T>(LerpFn<T> fn)
    {
        return (from, to, f) => fn(from, to, f * f);
    }

    /// <summary>
    ///     Wraps a linear-interpolation function so that it eases out instead of linearly interpolates.
    /// </summary>
    public static LerpFn<T> EaseOut<T>(LerpFn<T> fn)
    {
        return (from, to, f) => fn(from, to, 1 - (1 - f) * (1 - f));
    }

    /// <summary>
    ///     Wraps a linear-interpolation function so that it eases in and out instead of linearly interpolates.
    /// </summary>
    public static LerpFn<T> EaseInOut<T>(LerpFn<T> fn)
    {
        var easedIn = EaseIn(fn);
        var easedOut = EaseOut(fn);
        return (from, to, f) => fn(easedIn(from, to, f), easedOut(from, to, f), f);
    }
    
    /// <summary>
    ///     Calculates the distance between world points [a] and [b]
    ///     on a sphere centered at world point [origin] with radius [r]
    /// </summary>
    public static float SphericalDistance(Vector3 origin, Vector3 a, Vector3 b, float r)
    {
        return r * Mathf.Acos(Vector3.Dot(a - origin, b - origin) / (r * r));
    }
}

/// <summary>
///     Provides utility functions that are extensions on
///     other classes
/// </summary>
public static class UtilityExtensions
{
    private static readonly Dictionary<GameObject, Vector3> _originalScales = new();

    /// <summary>
    ///     Calls the given [fn] after a number of [seconds]
    /// </summary>
    public static Coroutine WaitThen(this MonoBehaviour behaviour, float seconds, Utility.Thunk fn)
    {
        return behaviour.StartCoroutine(Utility.WaitThen(seconds, fn));
    }

    /// <summary>
    ///     Calls the given [fn] after the [first] coroutine finishes
    /// </summary>
    public static Coroutine WaitThen(this MonoBehaviour behaviour, Coroutine first, Utility.Thunk fn)
    {
        return behaviour.StartCoroutine(Utility.WaitThen(first, fn));
    }

    /// <summary>
    ///     Automatically lerps [from] a value [to] another over [totalSeconds] time, using the provided
    ///     [lerp] function and assigning the value using a [callback].
    /// </summary>
    public static Coroutine AutoLerp<T>(this MonoBehaviour behaviour, T from, T to, float totalSeconds,
        Utility.LerpFn<T> lerp, Utility.LerpValueCallback<T> callback)
    {
        return behaviour.StartCoroutine(Utility.Lerp(from, to, totalSeconds, lerp, callback));
    }

    /// <summary>
    ///     Returns true if the given [worldPosition] is inside this [camera]'s frustum
    ///     (i.e. if it is in view)
    /// </summary>
    public static bool IsInView(this Camera camera, Vector3 worldPosition)
    {
        var position = camera.WorldToScreenPoint(worldPosition);

        return position.x > 0 && position.y > 0 && position.z > 0
               && position.x < camera.pixelWidth && position.y < camera.pixelHeight;
    }

    /// <summary>
    ///     Returns _a_ vector that is orthogonal to this [vector]
    /// </summary>
    public static Vector3 OrthogonalVector(this Vector3 vector)
    {
        var cross = Vector3.Cross(vector, Vector3.up);
        if (cross.magnitude == 0) return Vector3.Cross(vector, Vector3.forward);

        return cross;
    }

    /// <summary>
    ///     Sets this gameObject to be "visible" or "invisible" by setting
    ///     its local scale to Vector3.zero.
    ///     Keeps track of previous scales and can restore them, too!
    /// </summary>
    public static void SetVisible(this GameObject gameObject, bool visible)
    {
        var localScale = gameObject.transform.localScale;
        switch (visible)
        {
            case false when localScale != Vector3.zero:
                _originalScales[gameObject] = localScale;
                localScale = Vector3.zero;
                gameObject.transform.localScale = localScale;
                break;

            case true when localScale == Vector3.zero && _originalScales.ContainsKey(gameObject):
                gameObject.transform.localScale = _originalScales[gameObject];
                _originalScales.Remove(gameObject);
                break;
        }
    }

    /// <summary>
    ///     Returns true if this [gameObject] is "visible," i.e.
    ///     has a localScale not equal to zero.
    /// </summary>
    public static bool IsVisible(this GameObject gameObject)
    {
        return gameObject.transform.localScale != Vector3.zero;
    }
    
    /// <summary>
    ///     Returns a world-space boundary that encapsulates all the
    ///     renderers inside or on the given gameObject
    /// </summary>
    public static Bounds? GetWorldBounds(this GameObject gameObject)
    {
        var boundary = new Bounds();
        var hasBoundary = false;

        foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>())
            if (!hasBoundary)
            {
                hasBoundary = true;
                boundary = renderer.bounds;
            }
            else
            {
                boundary.Encapsulate(renderer.bounds);
            }

        return hasBoundary ? boundary : null;
    }

}