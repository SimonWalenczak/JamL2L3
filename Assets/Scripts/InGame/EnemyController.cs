using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public UpgradeManager _upgradeManager;
    [SerializeField] float _speed = 4f;
    [SerializeField] int _xp = 4;
    [SerializeField] int _score = 10;

    private GameObject _player;
    private Rigidbody2D _rb;

    [Header("Invincibility frames")]
    [SerializeField] private float invincibilityTime;
    [System.NonSerialized] public bool isTouched = false;   
    private float timer;

    [Header("Attack Force")]
    [SerializeField] private float attackForce;

    private bool dead;
    private bool deadInHole = false;


    [SerializeField] Vector3 scaleChange = new Vector3(-0.1f, -0.1f, -0.1f);
    [SerializeField] float rotationSpeed = 2f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        Initialize(player);
        MainGameplay.Instance.AddToList(this.gameObject);
        _upgradeManager = UpgradeManager.Instance;
    }

    public void Initialize( GameObject player )
    {
        _player = player;
    }
    
    void Update()
    {
        if (!dead)
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
        else if (dead && !deadInHole)
        {
            this.transform.Rotate(new Vector3(0, 0, rotationSpeed), Space.World);
        }
        else if (dead && deadInHole)
        {
            this.transform.localScale += scaleChange;
            if (transform.localScale.x <= 0)
            {
                transform.localScale = Vector3.zero;
            }
            _rb.velocity = Vector2.zero;
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

    public void Die()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Destroy(collider);
        dead = true;
        Destroy(this.gameObject, 5);

        _rb.velocity = (transform.position - _player.transform.position) * 1.5f;
        GameData._kill++;
        GameData._currentXp += _xp;
        GameData._score += (int)(_score * _upgradeManager.MultiplyScoreUpValue[_upgradeManager.MultiplyScoreIndex]);
    }
    
    public void DieHole()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Destroy(collider);
        dead = true;
        deadInHole = true;
        Destroy(this.gameObject, 2);

        _rb.velocity = (transform.position - _player.transform.position) * 1.5f;
        GameData._kill++;
        GameData._currentXp += _xp;
        GameData._score += (int)(_score * _upgradeManager.MultiplyScoreUpValue[_upgradeManager.MultiplyScoreIndex]);
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
                collision.gameObject.GetComponent<PlayerController>().lastEnemyTouched = this.gameObject;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * attackForce, ForceMode2D.Impulse);
            }
        }
    }
}
