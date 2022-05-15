namespace Core.UIFramework
{
    /// <summary>
    /// All inherited classes have to implement "Reset to default state" functionality
    /// </summary>
    public interface IResettable
    {
        public abstract void ResetState();
    }
}