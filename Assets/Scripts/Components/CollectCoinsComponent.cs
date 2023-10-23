using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Components
{
    public class CollectCoinsComponent : MonoBehaviour
    {
        public float _wallet;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Silver"))
            {
                _wallet += 1;
                Destroy(other.gameObject);
                Debug.Log($"You have {_wallet} coins!");
            } else if (other.CompareTag("Gold"))
            {
                _wallet += 10;
                Destroy(other.gameObject);
                Debug.Log($"You have {_wallet} coins!");
            }
        }
    }
}