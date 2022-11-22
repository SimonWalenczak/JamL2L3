using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnIndicator : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    [SerializeField] GameObject enemy;

    private void Start()
    {
        Destroy(this.gameObject, timeToSpawn);
    }

    private void OnDestroy()
    {
        Instantiate(enemy, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.identity);
    }
}
