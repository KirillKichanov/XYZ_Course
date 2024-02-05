using System;
using Scripts.Components.Audio;
using Scripts.Components.ColliderBased;
using Scripts.Components.GoBased;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Creatures.Mobs
{
    public class TurretAI : MonoBehaviour
    {
        [SerializeField] public bool _isActive;
        [SerializeField] private Cooldown _shootCooldown;
        [SerializeField] private LayerCheck _vision;
        
        [SerializeField] private Animator[] _turrets;
        private Animator _currentTurret;
        private int _currentIndex = 0;
        
        private static readonly int Shoot = Animator.StringToHash("shoot");

        private PlaySoundsComponent Sounds;
        
        private void Awake()
        {
            _currentIndex = 0;
            _turrets = GetComponentsInChildren<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        private void Update()
        {
            if (_vision.IsTouchingLayer && _isActive && _shootCooldown.IsReady) 
            {
                RangeAttack();
            }
        }

        private void RangeAttack()
        {
            _currentTurret = _turrets[_currentIndex];

            if (_currentTurret != null)
            {
                _currentTurret.SetTrigger(Shoot);
                Sounds.Play("Range");
                _shootCooldown.SetCooldown();
            }
            
            _currentIndex++;

            if (_currentIndex >= _turrets.Length)
            {
                _currentIndex = 0;
            }
        }

        public void OnActivateTurret()
        {
            _isActive = !_isActive;
        }
    }
}