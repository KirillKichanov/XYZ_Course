using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Scripts.Components;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _damageJumpForce;
    [SerializeField] private int _damage;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _interactionLayer;


    [SerializeField] private int _coins;

    [SerializeField] private LayerCheck _groundCheck;

    [SerializeField] private AnimatorController _armed;
    [SerializeField] private AnimatorController _unarmed;
    
    [SerializeField] private CheckCircleOverlap _attackRange;

    [SerializeField] private SpawnComponent _footStepParticles;
    [SerializeField] private SpawnComponent _jumpParticles;
    [SerializeField] private SpawnComponent _landingParticles;
    [SerializeField] private ParticleSystem _hitParticles;

    private Collider2D[] _interactionResult = new Collider2D[1];
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private bool _isGrounded;
    private bool _allowDoubleJump;
    private bool _doubleJumpUsed;
    private bool _isArmed;
    private float _fallingDuration;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunningKey = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int AttackKey = Animator.StringToHash("attack");


    public float Speed => _speed; 
    public int Coins => _coins;//так доставать приватные переменные в тест
    /* public float JumpForce
    {
        get { return _jumpForce; } property C#
        private set { _jumpForce = value; }
    } */

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _coins = 0;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        var isGrounded = IsGrounded();
        if (_isGrounded == false && isGrounded)
        {
            if (_fallingDuration > 0.5 || _doubleJumpUsed == true)
                SpawnLandingParticles();
            _fallingDuration = 0;
            _doubleJumpUsed = false;
        }

        _isGrounded = isGrounded;
    }

    private void FixedUpdate()
    {
        var xVelocity = _direction.x * _speed;
        var yVelocity = CalculateYVelocity();
        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        if (yVelocity < 0)
        {
            _fallingDuration += Time.fixedDeltaTime;
        }

        _animator.SetBool(IsGroundKey, _isGrounded);
        _animator.SetFloat(VerticalVelocityKey, _rigidbody.velocity.y);
        _animator.SetBool(IsRunningKey, _direction.x != 0);

        UpdateSpriteDirection();
    }

    private float CalculateYVelocity()
    {
        var yVelocity = _rigidbody.velocity.y;
        var isJumpPressing = _direction.y > 0;

        if (_isGrounded) _allowDoubleJump = true;
        if (isJumpPressing)
        {
            yVelocity = CalculateJumpVelocity(yVelocity);
            if (_isGrounded && _rigidbody.velocity.y <= 0.001f)
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
        else if (_rigidbody.velocity.y > 0)
        {
            yVelocity *= 0.5f;
        }

        return yVelocity;
    }

    private float CalculateJumpVelocity(float yVelocity)
    {
        var isFalling = _rigidbody.velocity.y <= 0.001f;
        if (!isFalling) return yVelocity;

        if (_isGrounded)
        {
            yVelocity += _jumpForce;
            SpawnJumpParticles();
        }
        else if (_allowDoubleJump)
        {
            _doubleJumpUsed = true;
            yVelocity += _jumpForce;
            SpawnJumpParticles();
            _allowDoubleJump = false;
        }

        return yVelocity;
    }

    private void UpdateSpriteDirection()
    {
        if (_direction.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (_direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool IsGrounded()
    {
        return _groundCheck.IsTouchingLayer;
    }

    public void SaySomething()
    {
        Debug.Log("Something!");
    }

    public void CoinCollect(int Amount)
    {
        _coins += Amount;
        Debug.Log(_coins);
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(Hit);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpForce);

        if (_coins > 0)
        {
            SpawnCoins();
        }
    }

    private void SpawnCoins()
    {
        var numCoinsToDispose = Math.Min(_coins, 5);
        _coins -= numCoinsToDispose;

        var burst = _hitParticles.emission.GetBurst(0);
        burst.count = numCoinsToDispose;
        _hitParticles.emission.SetBurst(0, burst);

        _hitParticles.gameObject.SetActive(true);
        _hitParticles.Play();
    }

    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(
            transform.position,
            _interactionRadius,
            _interactionResult,
            _interactionLayer);

        for (int i = 0; i < size; i++)
        {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void SpawnFootDust()
    {
        _footStepParticles.Spawn();
    }

    public void SpawnJumpParticles()
    {
        _jumpParticles.Spawn();
    }

    public void SpawnLandingParticles()
    {
        _landingParticles.Spawn();
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
        _isArmed = true;
        _animator.runtimeAnimatorController = _armed;
    }

    public void Attack()
    {
        if (!_isArmed) return;
        
        _animator.SetTrigger(AttackKey);
    }

    public void OnAttack()
    {
        var gos = _attackRange.GetObjectsInRange();
        foreach (var go in gos)
        {
            var hp = go.GetComponent<HealthComponent>();
            if (hp != null && go.CompareTag("Enemy"))
            {
                hp.ApplyDamage(_damage);
            }
        }
    }
}