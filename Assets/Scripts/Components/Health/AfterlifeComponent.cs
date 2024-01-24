using System.Collections;
using Scripts.Components.Animations;
using UnityEngine;

namespace Scripts.Components.Health
{
    public class AfterlifeComponent : MonoBehaviour
    {
        [SerializeField] private Collider2D _aliveCollider;
        [SerializeField] private Collider2D _afterlifeCollider;
        
        private void ColliderChange()
        {
            _aliveCollider.enabled = false;
            _afterlifeCollider.enabled = true;
        }

        private void LayerChange()
        {
            gameObject.layer = LayerMask.NameToLayer("Trash");
        }
    }
}