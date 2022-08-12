namespace ChronoDivergence
{
    public interface IOpenable
    {
        bool IsOpened();
        bool IsActivatable();
        void SetActivatable(bool activatable);
    }
}