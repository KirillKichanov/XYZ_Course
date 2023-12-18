using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Components
{
    public class WindComponent : MonoBehaviour
    {
        [SerializeField] private float _windForce;
        [SerializeField] private Collider2D _inactiveWind;
        [SerializeField] private Collider2D _activeWind;

        private void Start()
        {
            _inactiveWind.enabled = true;
            _activeWind.enabled = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var rigidBody = other.gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.AddForce(-Vector2.right*_windForce*Time.deltaTime);
            }
        }

        private void WindActivate()
        {
            _inactiveWind.enabled = false;
            _activeWind.enabled = true;
        }
    }

}
