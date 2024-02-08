using System;
using UnityEngine;

namespace Scripts.Components.ColliderBased
{
    public class PushBackComponent : MonoBehaviour
    {
        private float _pushForce = 1000;
        [SerializeField] private float _forceMultiplier;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var rigidBody = other.gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.AddForce(Vector2.right*_pushForce*_forceMultiplier*Time.deltaTime);
            }
        }
    }
}