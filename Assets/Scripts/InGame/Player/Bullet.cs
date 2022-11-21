using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private Vector3 _direction;

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }
    void Start()
    {
        GameObject.Destroy(gameObject, 5);
    }
    
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
