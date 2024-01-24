using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using TowerDefense;

public class TimeLevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float _timeLimit = 4f;
    public bool IsComplited => Time.time > _timeLimit;
}
