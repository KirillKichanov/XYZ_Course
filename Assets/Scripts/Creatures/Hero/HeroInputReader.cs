using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Scripts.Creatures;

namespace Scripts.Creatures.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Interact();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Attack();
            }
        }

        /*public void OnDoThrow()
        {

        }*/

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Throw();
                _hero.CancelMultipleThrow();
            }
            else if (context.performed && _hero._swords >= 3)
            {
                _hero.MultipleThrow();
            }
        }
        
        public void OnHealingPotion(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.UseHealingPotion();
            }
        }

    }
}
