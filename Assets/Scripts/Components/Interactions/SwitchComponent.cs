using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components.Interactions
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;
        [SerializeField] private string _animationKey;
        
        public bool doorState => _state;

        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animationKey, _state);
        }

        [ContextMenu("Switch")]
        public void SwitchIt()
        {
            Switch();
        }
    }
}
