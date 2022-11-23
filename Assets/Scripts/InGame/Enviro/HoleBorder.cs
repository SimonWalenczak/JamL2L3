using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBorder : MonoBehaviour
{
    [SerializeField] private LayerMask enemies;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().isTouched == true)
            {
                collision.gameObject.GetComponent<PlayerController>().Die();
            }
        }
        else if (UnityExtensions.Contains(enemies, collision.gameObject.layer))
        {
            if (collision.gameObject.GetComponent<EnemyController>().isTouched == true)
            {
                collision.gameObject.GetComponent<EnemyController>().DieHole();
            }
        }

    }
}
