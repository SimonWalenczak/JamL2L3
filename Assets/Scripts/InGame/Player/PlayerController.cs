using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioManager _audioManager;
    private AudioSource _audioSource;
    public UiManager _uiManager;
    [SerializeField] GameObject _ultimateArea;
    [SerializeField] float ultimateTime;
    public float _speed = 5;
    [SerializeField] Rigidbody2D rb;

    private float _timerCoolDownAttack;

    public float _currentCoolDownUlt;
    public float _maxCurrentCoolDownUlt;

    [SerializeField] private GameObject _weapon;

    private Animator _animator;

    private Vector2 scale;
    
    [Header("Invincibility frames")]
    public float invincibilityTime;
    [System.NonSerialized] public bool isTouched = false;
    private float timer;

    private bool dead = false;
    public GameObject lastEnemyTouched = null;

    public LayerMask enemies;

    [SerializeField] private bool _canSoundWalk;
    [SerializeField] private bool _canSoundHit;
    [SerializeField] private bool _canSoundDead;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        scale = transform.localScale;
        _currentCoolDownUlt = _maxCurrentCoolDownUlt;
        _audioSource = GetComponent<AudioSource>();
        _canSoundWalk = true;
        _canSoundHit = true;
        _canSoundDead = true;
    }

    IEnumerator SoundWalkWaiting()
    {
        yield return new WaitForSeconds(0.5f);
        _canSoundWalk = true;
    }
    IEnumerator SoundHitWaiting()
    {
        yield return new WaitForSeconds(0.5f);
        _canSoundHit = true;
    }
    IEnumerator SoundDeadWaiting()
    {
        yield return new WaitForSeconds(0.5f);
        _canSoundDead = true;
    }
    
    void Update()
    {
        if (!dead)
        {
            if (!isTouched)
            {
                Move();
                //Shoot();
            }
            else
            {
                _animator.SetBool("_isHit", true);
                if (_canSoundHit)
                {
                    _canSoundHit = false;
                    _audioSource.clip = _audioManager.AudioClips[1];
                    _audioSource.Play();
                    StartCoroutine(SoundHitWaiting());
                }
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
                _currentCoolDownUlt = _maxCurrentCoolDownUlt;
            }
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
            if (_canSoundWalk)
            {
                _canSoundWalk = false;
                _audioSource.clip = _audioManager.AudioClips[0];
                _audioSource.Play();
                StartCoroutine(SoundWalkWaiting());
            }
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
        if (_canSoundDead)
        {
            _canSoundDead = false;
            _audioSource.clip = _audioManager.AudioClips[2];
            _audioSource.Play();
            StartCoroutine(SoundDeadWaiting());
        }
        dead = true;
        _weapon.SetActive(false);
        _animator.SetBool("_isDead", true);
        rb.velocity = (transform.position - lastEnemyTouched.transform.position) * 3f;
        StartCoroutine(SlideOut());
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

    IEnumerator SlideOut()
    {
        yield return new WaitForSeconds(2);
        _uiManager._slideOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
