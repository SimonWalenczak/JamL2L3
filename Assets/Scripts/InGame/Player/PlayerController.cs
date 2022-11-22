using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _prefabBullet;
    [SerializeField] float _speed = 5;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float _coolDownAttack = 2;
    private float _timerCoolDownAttack;

    public float CoolDownUlt = 10f;

    [Header("Invincibility frames")]
    [SerializeField] private float invincibilityTime;
    [System.NonSerialized] public bool isTouched = false;
    private float timer;

    private bool dead = false;
    public GameObject lastEnemyTouched = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!dead)
        {
            if (!isTouched)
            {
                Move();
                Shoot();
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

            if (Input.GetKeyDown(KeyCode.T))
            {
                //Activation de l'ultimate
                
            }
        }     
    }

    private void Shoot()
    {
        _timerCoolDownAttack += Time.deltaTime;

        if (_timerCoolDownAttack < _coolDownAttack)
            return;

        _timerCoolDownAttack -= _coolDownAttack;
        GameObject go = GameObject.Instantiate(_prefabBullet, transform.position, Quaternion.identity);

        EnemyController enemy = MainGameplay.Instance.GetClosestEnemy(transform.position);

        Vector3 direction = enemy.transform.position - transform.position;
        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            go.GetComponent<Bullet>().Initialize(direction);
        }

    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector2(horizontal, vertical);
        direction.z = 0;

        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            rb.velocity = direction * _speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Die()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Destroy(collider);
        dead = true;

        rb.velocity = (transform.position - lastEnemyTouched.transform.position) * 3f;
    }
}
