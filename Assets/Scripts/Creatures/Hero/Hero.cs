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

        [Header("Cooldowns")] 
        [SerializeField] private Cooldown _throwCooldown;

        [Header("Animators")] 
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        [Header("Particles")] 
        [SerializeField] private SpawnComponent _attack1Particles;
        [SerializeField] private ParticleSystem _hitParticles;

        [Header("ItemsPower")] 
        [SerializeField] public int _swords;
        [SerializeField] private int _healingPotionPower = 5;

        //**********
        // DoubleJump
        //**********
        private bool _allowDoubleJump;
        private bool _doubleJumpUsed;

        //**********
        // Coroutines
        //**********
        private Coroutine _multipleThrowCoroutine;

        //**********
        // Inventory
        //**********
        private int SwordCount => _session.Data.Inventory.Count("Sword");
        private int CoinCount => _session.Data.Inventory.Count("Coin");
        private int HealingPotionCount => _session.Data.Inventory.Count("HealingPotion");

        //**********
        //Animator Keys
        //**********
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
            _throwCooldown.Initialize();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
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

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Math.Min(CoinCount, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

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

        public override void Attack()
        {
            if (SwordCount <= 0) return;

            base.Attack();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _unarmed;
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
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
            if (_multipleThrowCoroutine != null) StopCoroutine(_multipleThrowCoroutine);
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

        public void UseHealingPotion()
        {
            var healthComponent = GetComponent<HealthComponent>();
            if (HealingPotionCount >= 1 && healthComponent.Health < healthComponent.MaxHealth)
            {
                healthComponent.ApplyHeal(_healingPotionPower);
                _session.Data.Inventory.Remove("HealingPotion", 1);
                _particles.Spawn("Heal");
            }
            else if (HealingPotionCount >= 1 && healthComponent.Health == healthComponent.MaxHealth)
            {
                //Placeholder for tooltip "HP is full"
                Debug.Log("Your HP is full!");
            }
        }
    }
}