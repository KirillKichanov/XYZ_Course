﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Components;
using Scripts.Components.Health;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ApplyDamageTest
    {
        [Test]
        public void ApplyDamageByHeroWithSimplePasses()
        {
            var player = LoadPrefabHelper.LoadHero();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.Health;

            healthComponent.ApplyDamage(1);

            var damage = healthComponent.Health;
            Assert.That(startHealth > damage, "Player can't take damage!");
            Debug.Log("Player takes damage successfully");

        }
    }
}
