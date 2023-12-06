using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _immortalTime;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDie;

        private DateTime _lastTakeDamage;
        public int health => _health;
        public int maxHealth => _maxHealth;

        public void ApplyDamage(int damageValue)
        {
            if (_lastTakeDamage.AddSeconds(_immortalTime) < DateTime.Now)
            {
                _lastTakeDamage = DateTime.Now;
                _health -= damageValue;
                _onDamage?.Invoke();
                if (_health <= 0)
                {
                    _onDie?.Invoke();
                }  
            }
        }

        public void ApplyHeal(int healValue)
        {
            _health += healValue;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            _onHeal?.Invoke();
        }
    }
}