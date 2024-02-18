namespace TowerDefense
{
    public enum Sound
    {
        BGM = 0,
        Arrow = 1,
        ArrowHit = 2,
        EnemyDie = 3,
        EnemyAttack = 4,
        PlayerWin = 5,
        PlayerLose = 6,
        Jazz = 7,
        Bomb = 8,
        BombHit = 9,
        AirDefense = 10,
        AirDefenseHit = 11
    }
    public static class SoundExtensions
    {
        public static void Play (this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}

