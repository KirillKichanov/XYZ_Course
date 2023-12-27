using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private OnOverlapEvent _onOverlap;
    private readonly Collider2D[] _interactionResult = new Collider2D[5];

    private void OnDrawGizmosSelected()
    {
        Handles.color = HandlesUtils.TransparentRed;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
    }

    public void Check()
    {
        var size = Physics2D.OverlapCircleNonAlloc(
            transform.position,
            _radius,
            _interactionResult,
            _mask);

        var overlaps = new List<GameObject>();
        for (var i = 0; i < size; i++)
        {
            _onOverlap?.Invoke(_interactionResult[i].gameObject);
        }
    }

    [Serializable]
    public class OnOverlapEvent : UnityEvent<GameObject>
    {
        
    }
}