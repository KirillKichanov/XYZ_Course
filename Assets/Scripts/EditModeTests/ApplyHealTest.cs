using System.Collections;
using NUnit.Framework;
using Scripts.Components;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ApplyHealTest
    {
        [Test]
        public void ApplyHealWithSimplePasses()
        {
            var player = LoadHeroHelper.LoadHero();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            
            healthComponent.ApplyDamage(1);
            var damagedHealth = healthComponent.health;
            healthComponent.ApplyHeal(1);
            var heal = healthComponent.health;
            
            Assert.That(heal > damagedHealth && heal == startHealth, "Player can't heal!");
            Debug.Log("Player's heal successful!");
        }
    }
}