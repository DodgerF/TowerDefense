namespace MyEventBus
{
    public class PlayerIsAttackedSignal
    {
        private float _dmg;
        public float Damage => _dmg;

        public PlayerIsAttackedSignal(float dmg)
        {
            
            _dmg = dmg;
        }
    }
}