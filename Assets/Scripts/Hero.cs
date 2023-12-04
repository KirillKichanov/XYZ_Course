using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Scripts.Components;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _damageJumpForce;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _interactionLayer;


    [SerializeField] private int _coins;

    [SerializeField] private LayerCheck _groundCheck;
    
    private Collider2D[] _interactionResult = new Collider2D[1];
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _isGrounded;
    private bool _allowDoubleJump;
    
    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunningKey = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
    private static readonly int Hit = Animator.StringToHash("hit");


    public float Speed => _speed; //так доставать приватные переменные в тест
    /* public float JumpForce
    {
        get { return _jumpForce; } property C#
        private set { _jumpForce = value; }
    } */

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
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
        _isGrounded = IsGrounded();
    }

    private void FixedUpdate()
    {
        var xVelocity = _direction.x * _speed;
        var yVelocity = CalculateYVelocity();
        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        
        
        
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
        } else if (_rigidbody.velocity.y > 0)
        {
            yVelocity *= 0.5f;;
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
        } else if (_allowDoubleJump)
        {
            yVelocity += _jumpForce;
            _allowDoubleJump = false;
        }

        return yVelocity;
    }

    private void UpdateSpriteDirection()
    {
        if (_direction.x > 0)
        {
            _sprite.flipX = false;
        } else if (_direction.x < 0)
        {
            _sprite.flipX = true;
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
}
