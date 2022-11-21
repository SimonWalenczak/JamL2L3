using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameplay : MonoBehaviour
{
    public static MainGameplay Instance;

    [SerializeField] GameObject _player;
    [SerializeField] List<EnemyController> _enemyControllers;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        foreach (var enemy in _enemyControllers)
        {
            enemy.Initialize(_player);
        }

    }

    public EnemyController GetClosestEnemy( Vector3 position  )
    {
        float bestDistance = float.MaxValue;
        EnemyController bestEnemy = null;

        foreach (var enemy in _enemyControllers)
        {
            Vector3 direction = enemy.transform.position - position;

            float distance = direction.sqrMagnitude;

            if ( distance < bestDistance)
            {
                bestDistance = distance;
                bestEnemy = enemy;
            }
        }

        return bestEnemy;
    }
}
