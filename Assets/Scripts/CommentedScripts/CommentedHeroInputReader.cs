using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommentedHeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    // объявление метода для вызова логики движения
    public void OnMovement(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>(); 
        _hero.SetDirection(direction);
    }
    // объявление метода для вызова логики говорилки
    public void OnSaySomething(InputAction.CallbackContext context)
    { 
        _hero.SaySomething();  
    }
    
}
