using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _speed = 4;

    private GameObject _player;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize( GameObject player )
    {
        _player = player;
    }
    
    void Update()
    {
        MoveToPlayer();

    }

    private void MoveToPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        direction.z = 0;

        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            _rb.velocity = direction * _speed;

        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }
}
