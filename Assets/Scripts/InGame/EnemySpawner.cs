using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("MinMax Values")]
    //[SerializeField] private Vector2 radiusMinMax;
    [SerializeField] private GameObject quad;
    [SerializeField] private List<GameObject> spawnPool;
    [SerializeField] private int numberToSpawn;

    [Header("Enemy Tick Rate")]
    [SerializeField] private float timeForSpawn;

    private void Start()
    {
        SpawnEnemy();
        StartCoroutine(TickSpawn());
    }

    IEnumerator TickSpawn()
    {
        yield return new WaitForSeconds(timeForSpawn);
        SpawnEnemy();
        StartCoroutine(TickSpawn());
    }

    

    public void SpawnEnemy()
    {
        int randomEnemy = 0;
        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float posX;
        float posY;
        Vector2 pos;

        for (int i = 0; i < numberToSpawn; i++)
        {
            randomEnemy = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomEnemy];

            posX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            posY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(posX, posY);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }

    }

    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusMinMax.x);
        Gizmos.DrawWireSphere(transform.position, radiusMinMax.y);
    }

    public void Spawn()
    {
        float X = Random.Range(radiusMinMax.x, radiusMinMax.y);
        float Y = Random.Range(radiusMinMax.x, radiusMinMax.y);

        float distanceFromSpawn = new Vector3(X + this.transform.position.x, Y + this.transform.position.y, 0f).sqrMagnitude;


        bool spawn = false;
        
        while (spawn == false)
        {
            if (distanceFromSpawn > radiusMinMax.y)
            {
                if (distanceFromSpawn < radiusMinMax.x)
                {
                    Debug.Log(distanceFromSpawn);
                    Instantiate(enemySpawn, new Vector3(X + this.transform.position.x, Y + this.transform.position.y, 0f), Quaternion.identity);
                    spawn = true;                   
                }
            }
            X = Random.Range(radiusMinMax.x, radiusMinMax.y);
            Y = Random.Range(radiusMinMax.x, radiusMinMax.y);
            Debug.Log(X + " & " + Y);

            distanceFromSpawn = new Vector3(X + this.transform.position.x, Y + this.transform.position.y, 0f).sqrMagnitude;
        }      
    }*/
}
