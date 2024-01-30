using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _immortalTime;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        private DateTime _lastTakeDamage;
        public int Health => _health;
        public int MaxHealth => _maxHealth;

        public void ApplyDamage(int damageValue)
        {
            if (_health <= 0) return;
            
            if (_lastTakeDamage.AddSeconds(_immortalTime) < DateTime.Now)
            {
                _lastTakeDamage = DateTime.Now;
                _health -= damageValue;
                _onChange?.Invoke(_health);
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
            _onChange?.Invoke(_health);
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
                _onChange?.Invoke(_health);
            }
            _onHeal?.Invoke();
        }

        public void SetHealth(int health)
        {
            _health = health;
        }
    }

    [Serializable]
    public class HealthChangeEvent : UnityEvent<int>
    {
    }
}