using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Components;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ApplyDamageTest
    {
        [Test]
        public void ApplyDamageByHeroWithSimplePasses()
        {
            var player = LoadHeroHelper.LoadHero();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;

            healthComponent.ApplyDamage(1);

            var damage = healthComponent.health;
            Assert.That(startHealth > damage, "Player can't take damage!");
            Debug.Log("Player takes damage successfully");

        }
    }
}
