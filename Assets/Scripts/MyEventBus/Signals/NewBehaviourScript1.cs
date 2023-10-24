namespace MyEventBus
{
    public class GoldHaveChangedSignal
    {
        private int _currentGold;
        public int CurrentGold => _currentGold;
        public GoldHaveChangedSignal(int currentGold)
        {
            _currentGold = currentGold;
        }
    }
}