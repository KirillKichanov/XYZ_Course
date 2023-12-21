using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class HealthModifierComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _heal;


        public void ApplyDamage()
        {
            var hero = GameManager.Instance.Hero;
            var healthComponent = hero.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_damage);
            }
        }

        public void ApplyHeal()
        {
            var hero = GameManager.Instance.Hero;
            var healthComponent = hero.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyHeal(_heal);
            }
        }
    }
}
