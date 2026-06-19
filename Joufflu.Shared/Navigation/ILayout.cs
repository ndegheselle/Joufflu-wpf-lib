namespace Joufflu.Shared.Navigation
{
    /// <summary>
    /// Page for navigation systems
    /// </summary>
    public interface IPage
    {
        public void OnShow()
        { }
        public void OnHide()
        { }
    }

    /// <summary>
    /// Page that can contain and display another page
    /// </summary>
    public interface ILayout : IPage
    {
        void Hide(IPage page);
        void Hide();

        void Show(IPage page);
        void Overlay();
    }
}
