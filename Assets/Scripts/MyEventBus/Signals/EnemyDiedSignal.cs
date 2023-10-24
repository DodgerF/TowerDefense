namespace MyEventBus
{
    public class EnemyDiedSignal
    {
        private int _gold;
        public int Gold => _gold;
        public EnemyDiedSignal(int gold)
        {
            _gold = gold;
        }
    }
}