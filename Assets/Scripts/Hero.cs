using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] public float _speed;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
    }

    /* private void Update()
    {
        if (_direction != Vector2.zero)
        {
            var delta = _direction * (_speed * Time.deltaTime);
            transform.position += new Vector3(delta.x, delta.y, 0);
        } 
    }
*/
    public void SaySomething()
    {
        Debug.Log("Something!");
    }
}
