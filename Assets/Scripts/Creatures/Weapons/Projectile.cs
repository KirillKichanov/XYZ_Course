using System;
using UnityEngine;

namespace Scripts.Creatures.Weapons
{
    public class Projectile : BaseProjectile
    { 
        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            position.x += Direction * _speed;
            Rigidbody.MovePosition(position);
        }
    }
}