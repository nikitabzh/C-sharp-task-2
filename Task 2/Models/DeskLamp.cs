namespace Task2App.Models
{
    public class DeskLamp : LightingDevice, IPluggable
    {
        public bool IsPluggedIn { get; private set; }

        public DeskLamp(string name, double breakProbability) : base(name, breakProbability) { }

        public void TogglePlug()
        {
            IsPluggedIn = !IsPluggedIn;
            if (!IsPluggedIn) IsOn = false;
        }

        public override void TurnOn()
        {
            if (CheckForBreakage()) return;
            if (IsPluggedIn) IsOn = true;
        }

        public override void TurnOff()
        {
            if (CheckForBreakage()) return;
            IsOn = false;
        }
    }
}