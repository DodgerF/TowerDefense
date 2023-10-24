namespace MyEventBus
{
    public class HPHaveChangedSignal
    {
        private float _currentHP;
        public float CurrentHP => _currentHP;
        public HPHaveChangedSignal(float currentHP)
        {
            _currentHP = currentHP;
        }
    }
}