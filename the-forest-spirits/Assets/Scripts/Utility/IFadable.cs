/// <summary>
///     Defines an object that can be faded in and out.
/// </summary>
public interface IFadable
{
    /// <summary>
    ///     Fades this object in. Returns the approximate it will take to do so.
    /// </summary>
    public void FadeIn();

    /// <summary>
    ///     Fades this object out. Returns the approximate it will take to do so.
    /// </summary>
    public void FadeOut();

    /// <summary>
    ///     Makes the object instantly visible
    /// </summary>
    public void CutIn();

    /// <summary>
    ///     Makes the object instantly invisible
    /// </summary>
    public void CutOut();
}