using System;
using UnityEngine;

namespace Scripts.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _value;

        private float _timesUp;
        private float _initialValue;

        public void SetCooldown()
        {
            _timesUp = Time.time + Value;
        }

        public bool IsReady => _timesUp <= Time.time;

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public void Initialize()
        {
            _initialValue = _value;
        }

        public void Reset()
        {
            _value = _initialValue;
        }
    }
}