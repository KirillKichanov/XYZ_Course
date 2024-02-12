using System;
using System.Collections;
using Scripts.Components.Animations;
using Scripts.Components.ColliderBased;
using Scripts.Components.GoBased;
using Scripts.Components.Health;
using Scripts.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace Scripts.Creatures.Mobs
{
    public class GhostAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _canAttack;
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private HealthModifierComponent _healthModifier;

        [Header("Teleport")] [SerializeField] private float _afterTeleportWaiting;
        [SerializeField] private Transform[] _points;

        private Coroutine _current;

        private Creature _creature;
        private Animator _animator;
        private Patrol _patrol;
        private SpawnListComponent _particles;
        private GameObject _target;

        private bool _isDead;
        private bool _isTeleported;
        private int _destinationPointIndex;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        private void Awake()
        {
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
            _particles = GetComponent<SpawnListComponent>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
            StartCoroutine(AuraChanger());
        }

        private void FixedUpdate()
        {
            var target = GameObject.Find("Hero");
            if (_canAttack.IsTouchingLayer)
            {
                _healthModifier.ApplyDamage(target);
                Teleport();
            }
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
        }

        public void Teleport()
        {
            var particles = GameObject.Find("TeleportParticles");
            if (particles != null) return;
            _particles.Spawn("teleport");

            StopCoroutine(_current);

            foreach (var sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color = new Color(1, 1, 1, 0);
            }

            _creature.SetDirection(new Vector3(0, 0, 0));

            var hero = GameObject.Find("Hero");
            var distance = 0f;
            var index = -1;
            for (var i = 0; i < _points.Length; i++)
            {
                var point = _points[i];
                var d = Vector3.Distance(hero.transform.position, point.position);
                if (d > distance)
                {
                    distance = d;
                    index = i;
                }
            }

            gameObject.transform.position = _points[index].position;

            _isTeleported = true;
            StartState(AfterTeleportWaiting());
        }

        private IEnumerator AfterTeleportWaiting()
        {
            yield return new WaitForSeconds(0.3f);
            _particles.Spawn("teleport");
            yield return new WaitForSeconds(0.3f);

            foreach (var sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color = new Color(1, 1, 1, 1);
            }

            _creature.SetDirection(new Vector3(0, 0, 0));
            yield return new WaitForSeconds(_afterTeleportWaiting);

            _isTeleported = false;
            StartState(_patrol.DoPatrol());
        }

        public void OnHealthChanged(int health)
        {
            if (health <= 1) 
                return;
            Teleport();
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                SetDirectionToTarget();
                yield return null;
            }

            _creature.SetDirection(Vector2.zero);
            StartState(_patrol.DoPatrol());
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

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead || _isTeleported) return;

            _target = go;

            StartState(GoToHero());
        }

        private IEnumerator AuraChanger()
        {
            var spriteAnimation = gameObject.GetComponentInChildren<SpriteAnimation>();
            bool previousValue = false;

            while (true)
            {
                if (previousValue == false && _vision.IsTouchingLayer)
                {
                    spriteAnimation.SetClip("danger-aura");
                }
                else if (previousValue && !_vision.IsTouchingLayer)
                {
                    spriteAnimation.SetClip("aura");
                }

                previousValue = _vision.IsTouchingLayer;
                yield return null;
            }
        }
    }
}