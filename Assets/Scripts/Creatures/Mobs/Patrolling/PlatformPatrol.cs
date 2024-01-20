using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Creatures.Mobs.Patrolling
{
    public class PlatformPatrol : Patrol
    {
        private Creature _creature;
        private Rigidbody2D _rigidbody;
        
        [SerializeField] private Collider2D _creatureCollider;
        
        private void Awake()
        {
            _creature = GetComponent<Creature>();
            _creature.SetDirection(Vector2.right);
        }
        public override IEnumerator DoPatrol()
        {
            var direction = Vector2.right;
            while (enabled)
            {
                var bounds = _creatureCollider.bounds;
                var leftOrigin = new Vector2(bounds.min.x, bounds.center.y);
                var rightOrigin = new Vector2(bounds.max.x, bounds.center.y);
                var maxDistance = bounds.extents.y + 0.5f;
                var layerMask = 1 << LayerMask.NameToLayer("Ground");
                
                if (direction.x < 0f && !Physics2D.Raycast(leftOrigin, Vector2.down, maxDistance, layerMask))
                {
                    direction = -direction;
                }
                if (direction.x > 0f && !Physics2D.Raycast(rightOrigin, Vector2.down, maxDistance, layerMask))
                {
                    direction = -direction;
                }
                _creature.SetDirection(direction.normalized);
                
                yield return null;
            }
        }
    }
}