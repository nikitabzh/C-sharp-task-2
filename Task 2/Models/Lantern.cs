namespace Task2App.Models
{
    public class Lantern : LightingDevice
    {
        public Lantern(string name, double breakProbability) : base(name, breakProbability) { }

        public override void TurnOn()
        {
            if (CheckForBreakage()) return;
            IsOn = true;
        }

        public override void TurnOff()
        {
            if (CheckForBreakage()) return;
            IsOn = false;
        }
    }
}