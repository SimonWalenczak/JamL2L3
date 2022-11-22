using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("MinMax Values")]
    [SerializeField] private Vector2 radiusMinMax;
    [SerializeField] private GameObject enemySpawn;

    [Header("Enemy Tick Rate")]
    [SerializeField] private float timeForSpawn;

    private void Start()
    {
        StartCoroutine(TickSpawn());
    }

    IEnumerator TickSpawn()
    {
        yield return new WaitForSeconds(timeForSpawn);
        Spawn();
        StartCoroutine(TickSpawn());
    }

    public void Spawn()
    {
        float X = Random.Range(radiusMinMax.x, radiusMinMax.y);
        float Y = Random.Range(radiusMinMax.x, radiusMinMax.y);

        Vector2 spawnPoint = new Vector2(X, Y);

        float distanceFromSpawn = Vector3.Distance(this.transform.position, spawnPoint);
        Debug.Log(distanceFromSpawn);
;
        bool spawn = false;
        
        while (spawn == true)
        {
            if (distanceFromSpawn > radiusMinMax.y)
            {
                if (distanceFromSpawn < radiusMinMax.x)
                {
                    Instantiate(enemySpawn, new Vector3(radiusMinMax.x, radiusMinMax.y, 0f), Quaternion.identity);
                    spawn = true;
                }
            }
        }

        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusMinMax.x);
        Gizmos.DrawWireSphere(transform.position, radiusMinMax.y);
    }
}
