namespace MyEventBus
{
    public class HPHaveChangedSignal
    {

        private float _oldHP;
        public float OldHP => _oldHP;

        private float _currentHP;
        public float CurrentHP => _currentHP;
        public HPHaveChangedSignal(float oldHP, float currentHP)
        {
            _oldHP = oldHP;
            _currentHP = currentHP;
        }
    }
}