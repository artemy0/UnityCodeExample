namespace System.Analytic.UserProperties
{
    public interface IUserProperty
    {
        string Name { get; }
        string Value { get; }
        void Release();
    }
}