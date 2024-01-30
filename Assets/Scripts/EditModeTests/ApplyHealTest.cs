using System.Collections;
using NUnit.Framework;
using Scripts.Components;
using Scripts.Components.Health;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ApplyHealTest
    {
        [Test]
        public void ApplyHealWithSimplePasses()
        {
            var player = LoadPrefabHelper.LoadHero();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.Health;
            
            healthComponent.ApplyDamage(1);
            var damagedHealth = healthComponent.Health;
            healthComponent.ApplyHeal(1);
            var heal = healthComponent.Health;
            
            Assert.That(heal > damagedHealth && heal == startHealth, "Player can't heal!");
            Debug.Log("Player's heal successful!");
        }
    }
}