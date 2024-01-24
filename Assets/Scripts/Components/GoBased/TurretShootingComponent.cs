using UnityEngine;

namespace Scripts.Components.GoBased
{
    public class TurretShootingComponent : MonoBehaviour
    {
        [SerializeField] private SpawnComponent _shoot;
        
        public void OnShoot()
        {
            _shoot.Spawn();
        }
    }
}