using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameplay : MonoBehaviour
{
    public static MainGameplay Instance;
    
    [SerializeField] List<EnemyController> _enemyControllers;
    
    private void Awake()
    {
        Instance = this;

        GameData.ShootPower = 1;
        GameData.MultipScoring = 1;
        GameData._coolDownUltReduc = 0;
        GameData._kill = 0;
        GameData._currentXp = 0;
        GameData._totalXp = 0;
    }

    public void AddToList(GameObject enemy)
    {
        _enemyControllers.Add(enemy.GetComponent<EnemyController>());
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


public static class UnityExtensions
{

    /// <summary>
    /// Extension method to check if a layer is in a layermask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}