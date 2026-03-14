using System;

namespace Task2App.Models
{
    public abstract class LightingDevice
    {
        private readonly double _breakProbability;
        private static readonly Random Rnd = new Random();

        public string Name { get; }
        public bool IsOn { get; protected set; }
        public bool IsBroken { get; private set; }

        public virtual string StatusText => IsOn ? "Включено" : "Выключено";

        public event EventHandler? Broken;

        protected LightingDevice(string name, double breakProbability)
        {
            Name = name;
            _breakProbability = breakProbability;
            IsOn = false;
            IsBroken = false;
        }

        public abstract void TurnOn();
        public abstract void TurnOff();

        protected void TriggerBrokenEvent()
        {
            if (IsBroken) return;
            
            IsBroken = true;
            IsOn = false;
            
            Broken?.Invoke(this, EventArgs.Empty);
        }

        protected bool CheckForBreakage()
        {
            if (IsBroken) return true;
            
            if (Rnd.NextDouble() < _breakProbability)
            {
                TriggerBrokenEvent();
                return true;
            }
            return false;
        }
        
        public void Repair()
        {
            IsBroken = false;
        }
    }
}