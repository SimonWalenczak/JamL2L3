using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _prefabBullet;
    [SerializeField] GameObject _ultimateArea;
    [SerializeField] float ultimateTime;
    [SerializeField] float _speed = 5;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float _coolDownAttack = 2;
    private float _timerCoolDownAttack;

    public  float _currentCoolDownUlt = GameData.CoolDownUlt;

    private Animator _animator;

    private Vector2 scale;
    
    [Header("Invincibility frames")]
    [SerializeField] private float invincibilityTime;
    [System.NonSerialized] public bool isTouched = false;
    private float timer;

    private bool dead = false;
    public GameObject lastEnemyTouched = null;

    public LayerMask enemies;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        scale = transform.localScale;
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
                _animator.SetBool("_isHit", true);
                timer += Time.deltaTime;
                if (timer >= invincibilityTime)
                {
                    timer = 0;
                    isTouched = false;
                    _animator.SetBool("_isHit", false);
                }
            }

            _currentCoolDownUlt -= Time.deltaTime;
            
            if (_currentCoolDownUlt <= 0 && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Ultimate());
                _currentCoolDownUlt = GameData.CoolDownUlt;
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

    IEnumerator Ultimate()
    {
        _ultimateArea.SetActive(true);
        yield return new WaitForSeconds(ultimateTime);
        _ultimateArea.SetActive(false);
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector2(horizontal, vertical);
        direction.z = 0;

        if (direction.sqrMagnitude > 0)
        {
            _animator.SetBool("_isMoving", true);
            direction.Normalize();
            rb.velocity = direction * _speed;

            if (direction.x > 0)
            {
                transform.localScale = new Vector2(scale.x, scale.y);
            }
            if (direction.x < 0)
            {
                transform.localScale = new Vector2(-scale.x, scale.y);
            }
        }
        else
        {
            _animator.SetBool("_isMoving", false);
            rb.velocity = Vector2.zero;
        }
    }

    public void Die()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Destroy(collider);
        dead = true;
        _animator.SetBool("_isDead", true);
        rb.velocity = (transform.position - lastEnemyTouched.transform.position) * 3f;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (UnityExtensions.Contains(enemies, collision.gameObject.layer))
        {
            if (collision.gameObject.GetComponent<EnemyController>().isTouched == true)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.transform.position - transform.position, ForceMode2D.Force);
            }
        }
    }
}
