using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Scripts.Components;
using Scripts.Components.ColliderBased;
using Scripts.Components.GoBased;
using Scripts.Components.Health;
using Scripts.Model;
using Scripts.Utils;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Creatures.Hero
{
    public class Hero : Creature
    {
        private GameSession _session;

        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;
        
        [SerializeField] private SpawnComponent _attack1Particles;
        [SerializeField] private ParticleSystem _hitParticles;

        [SerializeField] public int _swords;

        private bool _allowDoubleJump;
        private bool _doubleJumpUsed;
        private Coroutine _multipleThrowCoroutine;
        
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        
        //public int Coins => _coins;//так доставать приватные переменные в тест
        /* public float JumpForce
        {
            get { return _jumpForce; } property C#
            private set { _jumpForce = value; }
        } */


        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();
            health.SetHealth(_session.Data.Hp);

            UpdateHeroWeapon();
            _throwCooldown.Initialize();
        }
        
        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }

        protected override void Update()
        {
            base.Update();
            
            var isGrounded = _groundCheck.IsTouchingLayer;
            if (IsGrounded == false && isGrounded)
            {
                if (FallingDuration > 0.5 || _doubleJumpUsed == true)
                    _particles.Spawn("Landing");
                FallingDuration = 0;
                _doubleJumpUsed = false;
            }

            IsGrounded = isGrounded;
        }

        protected override bool AllowJump() => base.AllowJump() || _allowDoubleJump;

        protected override float CalculateYVelocity()
        {
            if (IsGrounded) _allowDoubleJump = true;

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (IsJumpPerformed) return yVelocity;

            yVelocity = base.CalculateJumpVelocity(yVelocity);
            if (AllowJump() && !base.AllowJump())
            {
                _doubleJumpUsed = true;
                _allowDoubleJump = false;
            }
            return yVelocity;
        }

        public void CoinCollect(int Amount)
        {
            _session.Data.Coins += Amount;
            Debug.Log(_session.Data.Coins);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (_session.Data.Coins > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Math.Min(_session.Data.Coins, 5);
            _session.Data.Coins -= numCoinsToDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        public void SpawnAttack1Particles()
        {
            _attack1Particles.Spawn();
        }

        public void ArmHero()
        {
            _session.Data.isArmed = true;
            UpdateHeroWeapon();
        }

        public override void Attack()
        {
            if (!_session.Data.isArmed) return;

            base.Attack();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _session.Data.isArmed ? _armed : _unarmed;
        }

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");
        }

        public void Throw()
        {
            if (_throwCooldown.IsReady && _swords > 1)
            {
                Animator.SetTrigger(ThrowKey);
                _throwCooldown.SetCooldown();
                _swords--;
            }
        }

        public void MultipleThrow()
        {
            if (_swords == 1) return;
            if (_multipleThrowCoroutine == null)
            {
                _multipleThrowCoroutine = StartCoroutine(OnMultipleThrow());
            }
        }

        public void CancelMultipleThrow()
        {
            if(_multipleThrowCoroutine != null) StopCoroutine(_multipleThrowCoroutine);
            _throwCooldown.Reset();
            _multipleThrowCoroutine = null;
        }

        private IEnumerator OnMultipleThrow()
        {
            while (_swords > 1)
            {
                if (_throwCooldown.IsReady)
                {
                    _throwCooldown.Value /= 2f;
                }
                Throw();
                yield return null;
            }

            _throwCooldown.Reset();
            _multipleThrowCoroutine = null;
        }
    }
}