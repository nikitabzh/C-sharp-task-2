namespace Task2App.Models
{
    public interface IPluggable
    {
        bool IsPluggedIn { get; }
        void TogglePlug();
    }
}