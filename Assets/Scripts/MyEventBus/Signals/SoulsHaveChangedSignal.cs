namespace MyEventBus
{
    public class SoulsHaveChangedSignal
    {
        private int _currentSouls;
        public int CurrentSouls => _currentSouls;
        public SoulsHaveChangedSignal(int currentSouls)
        {
            _currentSouls = currentSouls;
        }
    }
}