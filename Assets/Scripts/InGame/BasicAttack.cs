using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] private LayerMask enemies;
    [SerializeField] private Transform pivot;
    [SerializeField] private float attackForce;

    

    private void Start()
    {
        pivot = GetComponentInParent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemies)
        {
            Vector3 direction = collision.gameObject.transform.position - pivot.position;
            
            if (direction.sqrMagnitude > 0)
            {
                direction.Normalize();
                collision.GetComponent<Rigidbody2D>().AddForce(direction * attackForce, ForceMode2D.Force);
            }
        }       
    }
}
