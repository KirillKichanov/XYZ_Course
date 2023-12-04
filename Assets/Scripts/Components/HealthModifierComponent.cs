using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class HealthModifierComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _heal;


        public void ApplyDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_damage);
            }
        }

        public void ApplyHeal(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyHeal(_heal);
            }
        }
    }
}
