using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scripts.Utils;

namespace Scripts.Components
{


    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _action;
        
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.gameObject.IsInLayer(_layer))
            {
                _action?.Invoke(other.gameObject);
            }

            if (!string.IsNullOrEmpty(_tag) && other.gameObject.CompareTag(_tag))
            {
                _action?.Invoke(other.gameObject);
            }
        }
        

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
    
    /*public static class GameObjectExtensions
    {
        public static bool IsInLayer(this GameObject obj, int layer)
        {
            return (obj.layer == layer) || ((obj.layer & (1 << layer)) != 0);
        }
    }*/
}