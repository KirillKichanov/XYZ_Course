using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CommentedHero : MonoBehaviour
{
    // объявление переменной, содержащей значение скорости для рассчета дельты
    [SerializeField] private float _speed;
    // объявление вектора для получения направления движения 
    private Vector2 _direction;
    // объявление метода, который обновляет вектор при его изменении
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    // проверка того, что направление не 0 --- рассчет дельты для того, чтобы отвязать движение от фпс 
    // --- к текущей позиции добавляется дельта
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
