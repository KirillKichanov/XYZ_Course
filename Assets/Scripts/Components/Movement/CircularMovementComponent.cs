using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Components.Movement
{
    [ExecuteInEditMode] 
    public class CircularMovementComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private float _radius = 1.0f;
        private float angle;
        private float angleStep;

        private List<Transform> _children;

        void Start()
        {
            angleStep = 360.0f / transform.childCount;
            _children = new List<Transform>();
            _children.AddRange(gameObject.GetComponentsInChildren<Transform>());
            _children.Remove(transform);
        }

        void Update()
        {
            if (!Application.isPlaying)
                Start();
            if (_children == null)
                return;
            angle += _speed * Time.deltaTime;
            if (angle > 360.0f)
            {
                angle -= 360.0f;
            }

            var index = 0;
            
            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];
                if (child == null)
                    continue;
                float childAngle = angle + i * angleStep;
                if (childAngle > 360.0f)
                {
                    childAngle -= 360.0f;
                }

                Vector3 childPosition = transform.GetChild(index).localPosition;
                childPosition.x = Mathf.Cos(childAngle * Mathf.Deg2Rad) * _radius;
                childPosition.y = Mathf.Sin(childAngle * Mathf.Deg2Rad) * _radius;
                transform.GetChild(index).localPosition = childPosition;

                index++;
            }
            
            if (index == 0)
                Destroy(gameObject);
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}