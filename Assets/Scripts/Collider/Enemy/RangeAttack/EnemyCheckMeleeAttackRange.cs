using GameRPG;
using UnityEngine;

public class EnemyCheckMeleeAttackRange : MonoBehaviour
{
    private Goblin _goblin;

    private void Start()
    {
        _goblin = GetComponentInParent<Goblin>();
    }

}
