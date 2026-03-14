namespace Task2App.Models
{
    public class Chandelier : LightingDevice
    {
        private int _currentMode = 0;

        public Chandelier(string name, double breakProbability) : base(name, breakProbability) { }

        public override string StatusText => _currentMode switch
        {
            0 => "Выключено",
            1 => "Включена 1-я часть лампочек",
            2 => "Включена 2-я часть лампочек",
            3 => "Включены ВСЕ лампочки",
            _ => "Неизвестно"
        };

        public override void TurnOn()
        {
            if (CheckForBreakage()) return;
            
            if (_currentMode < 3)
            {
                _currentMode++;
                IsOn = true;
            }
        }

        public override void TurnOff()
        {
            if (CheckForBreakage()) return;

            if (_currentMode > 0)
            {
                _currentMode--;
                if (_currentMode == 0) IsOn = false;
            }
        }
    }
}