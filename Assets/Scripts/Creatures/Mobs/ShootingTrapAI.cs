using System;
using Scripts.Components.Audio;
using Scripts.Components.ColliderBased;
using Scripts.Components.GoBased;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;

        [Header("Melee")]
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;
        [SerializeField] private Cooldown _meleeCooldown;
        
        [Header("Range")]
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;

        private static readonly int Melee = Animator.StringToHash("melee");
        private static readonly int Range = Animator.StringToHash("range");
        
        private PlaySoundsComponent Sounds;
        
        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        private void Update()
        {
            if (_vision.IsTouchingLayer && _meleeCanAttack.IsTouchingLayer && _meleeCooldown.IsReady)
            {
                MeleeAttack();
                Sounds.Play("Melee");
                return;
            }

            if (_vision.IsTouchingLayer && _rangeCooldown.IsReady && !_meleeCanAttack.IsTouchingLayer)
            {
                RangeAttack();
                Sounds.Play("Range");
            }
        }

        private void RangeAttack()
        {
            _rangeCooldown.SetCooldown();
            _animator.SetTrigger(Range);
        }

        private void MeleeAttack()
        {
            _meleeCooldown.SetCooldown();
            _animator.SetTrigger(Melee);
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}