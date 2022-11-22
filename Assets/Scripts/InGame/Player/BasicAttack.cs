using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] private LayerMask enemies;
    [SerializeField] private Transform pivot;
    [SerializeField] public float attackForce;

    private void Start()
    {
        pivot = GetComponentInParent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( UnityExtensions.Contains(enemies, collision.gameObject.layer))
        {
            Vector3 direction = collision.gameObject.transform.position - pivot.position;
            direction.Normalize();

            if (collision.gameObject.GetComponent<EnemyController>().isTouched == false)
            {
                collision.gameObject.GetComponent<EnemyController>().isTouched = true;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * attackForce, ForceMode2D.Impulse);
            }          
        }       
    }    
}
