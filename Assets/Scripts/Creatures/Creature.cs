using System;
using Scripts.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")] 
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpForce;
        [SerializeField] private float _damageVelocity;

        [Header("Checkers")]
        [SerializeField] protected LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;
        
        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected bool IsGrounded;
        protected float FallingDuration;
        
        protected bool IsJumpPressing => Direction.y > 0;
        protected bool IsJumpPerformed => IsJumpPressing && Rigidbody.velocity.y > 0;
        private bool IsJumpCanceled => !IsJumpPressing && Rigidbody.velocity.y > 0;
        protected virtual bool AllowJump() => IsGrounded;
        
        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
            
            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VerticalVelocityKey, Rigidbody.velocity.y);
            Animator.SetBool(IsRunningKey, Direction.x != 0);
        }
        
        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            if (yVelocity < 0)
            {
                FallingDuration += Time.fixedDeltaTime;
            }

            UpdateSpriteDirection(Direction);
        }
        
        protected virtual float CalculateYVelocity()
        {
            float yVelocity = Rigidbody.velocity.y;
            if (IsJumpPressing) 
                yVelocity = CalculateJumpVelocity(yVelocity);
            if (IsJumpCanceled)
                yVelocity *= 0.5f;

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (IsJumpPerformed) return yVelocity;
            if (!AllowJump()) return yVelocity;
    
            _particles.Spawn("Jump");
            return yVelocity + _jumpForce;
        }
        
        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1;
            if (Direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
            }
            else if (Direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
            }
        }
        
        public virtual void TakeDamage()
        {
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }
        
        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
        }
        
        public void OnDoAttack()
        {
            _attackRange.Check();
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
    }
}