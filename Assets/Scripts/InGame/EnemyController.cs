using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _speed = 4;
    [SerializeField] float _weight = 1;
    [SerializeField] int _xp = 4;

    private GameObject _player;
    private Rigidbody2D _rb;

    [Header("Invincibility frames")]
    [SerializeField] private float invincibilityTime;
    [System.NonSerialized] public bool isTouched = false;   
    private float timer;

    [Header("Attack Force")]
    [SerializeField] private float attackForce;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _weight = GetComponent<Rigidbody2D>().mass;
    }

    public void Initialize( GameObject player )
    {
        _player = player;
    }
    
    void Update()
    {
        if (!isTouched)
        {
            MoveToPlayer();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= invincibilityTime)
            {
                timer = 0;
                isTouched = false;
            }
        }
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Vector3 direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();

            if (collision.gameObject.GetComponent<PlayerController>().isTouched == false)
            {
                collision.gameObject.GetComponent<PlayerController>().isTouched = true;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * attackForce, ForceMode2D.Impulse);
            }
        }
    }
}
