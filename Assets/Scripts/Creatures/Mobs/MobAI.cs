﻿using System;
using System.Collections;
using Scripts.Components;
using Scripts.Components.ColliderBased;
using Scripts.Components.GoBased;
using Scripts.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace Scripts.Creatures.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [SerializeField] private float _alarmDelay;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _missCooldown;
        
        private Coroutine _current;
        private GameObject _target;
        
        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");
        private static readonly int IsDisappearKey = Animator.StringToHash("is-disappear");

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private bool _isDead;
        private Patrol _patrol;

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if(_isDead) return;
            
            _target = go;
            
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);
            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }
            
            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missCooldown);
            
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }
            
            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            direction.y = 0;
            _creature.SetDirection(direction);
        }
        
        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);
            
            if (_current != null)
                    StopCoroutine(_current);

            _current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _creature.SetDirection(Vector2.zero);
            _isDead = true;
            _animator.SetBool(IsDeadKey, true);

            if (_current != null)
            {
                StopCoroutine(_current);
            }
            
            StartState(CorpseDisappear());
        }

        private IEnumerator CorpseDisappear()
        {
            yield return new WaitForSeconds(2);
            _animator.SetBool(IsDisappearKey, true);
            yield return new WaitForSeconds(0.4f);
            Destroy(gameObject);
            StopCoroutine(_current);
        }
    }
}