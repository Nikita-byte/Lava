using System;
using UnityEngine;


public class EnemyPart : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public void OffAnimator()
    {
        _enemy.Die();
    }
}
