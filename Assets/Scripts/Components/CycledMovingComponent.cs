using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Components
{
    public class CycledMovingComponent : MonoBehaviour
    {
        [SerializeField] private bool _isActive;
        public bool isPlatformActive => _isActive;
        
        [SerializeField] private Transform _pointA;
        private Vector3 _instancePointA;
        [SerializeField] private Transform _pointB;
        private Vector3 _instancePointB;
        [SerializeField] private float _speed;

        private Vector3 _currentTarget;

        private void Start()
        {
            _instancePointA = _pointA.position;
            _instancePointB = _pointB.position;
            _currentTarget = _instancePointA;
        }

        private void Update()
        {
            if (_isActive)
            {
                MoveTo();
            }
        }

        private void MoveTo()
        {
            if (transform.position == _instancePointA)
            {
                _currentTarget = _instancePointB;
            }
            else if (transform.position == _instancePointB)
            {
                _currentTarget = _instancePointA;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }

        public void OnActivate()
        {
            _isActive = !_isActive;
        }
    }
}

