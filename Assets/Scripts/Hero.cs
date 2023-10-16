using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _direction;
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        if (_direction != Vector2.zero)
        {
            var delta = _direction * (_speed * Time.deltaTime);
            transform.position += new Vector3(delta.x, delta.y, 0);
        } 
    }

    public void SaySomething()
    {
        Debug.Log("Something!");
    }
}
