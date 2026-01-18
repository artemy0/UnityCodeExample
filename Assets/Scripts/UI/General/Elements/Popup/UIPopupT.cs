namespace UI.General.Elements.Popup
{
    public abstract class UIPopupT<T> : UIPopup
    {
        public abstract void Initialize(T data);
    }
}