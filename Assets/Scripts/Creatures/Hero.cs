using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Scripts.Components;
using Scripts.Model;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Creatures
{
    public class Hero : Creature
    {
        private GameSession _session;

        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        [SerializeField] private SpawnComponent _attack1Particles;
        [SerializeField] private ParticleSystem _hitParticles;

        private Collider2D[] _interactionResult = new Collider2D[1];
        private bool _allowDoubleJump;
        private bool _doubleJumpUsed;
        
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

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded) _allowDoubleJump = true;
            if (!isJumpPressing)
            {
                return 0f;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded && _allowDoubleJump)
            {
                _particles.Spawn("Jump");
                _doubleJumpUsed = true;
                _allowDoubleJump = false;
                return _jumpForce;
            }

            return base.CalculateJumpVelocity(yVelocity);
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

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<CycledMovingComponent>())
            {
                transform.parent = other.transform;
            }
        }

        public void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<CycledMovingComponent>())
            {
                transform.parent = null;
            }
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
    }
}