namespace MyEventBus
{
    public class EnemyKilledSignal
    {
        private int _gold;
        public int Gold => _gold;
        public EnemyKilledSignal(int gold)
        {
            _gold = gold;
        }
    }
}