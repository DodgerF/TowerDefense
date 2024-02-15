using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public enum GameStatus
    {
        Base,
        Aim
    }
    public class StatusMachine : MonoBehaviour
    {
        public static GameStatus currentStatus = GameStatus.Base;
    }

}
