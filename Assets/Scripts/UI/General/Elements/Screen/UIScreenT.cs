namespace UI.General.Elements.Screen
{
    public abstract class UIScreenT<T> : UIScreen
    {
        public abstract void Initialize(T data);
    }
}