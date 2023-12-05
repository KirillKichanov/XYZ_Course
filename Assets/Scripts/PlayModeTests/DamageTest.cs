﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Components;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class DamageTest: InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene(0);
        }
        

        [UnityTest]
        public IEnumerator DamageTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var heroComponent = player.GetComponent<Hero>();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            var spikes = GameObject.Find("Spikes (1)").transform.position;
            
            yield return new WaitForSeconds(1);

            healthComponent.ApplyDamage(1);
            heroComponent.TakeDamage();
            var firstDamage = healthComponent.health;
            Assert.That(startHealth > firstDamage, "Player can't take damage!");
            Debug.Log("Player takes first damage successfully");
            
            yield return new WaitForSeconds(1);

            player.transform.position = spikes;
            
            yield return new WaitForSeconds(1);

            var secondDamage = healthComponent.health;
            Assert.That(firstDamage > secondDamage, "Player can't take damage by spikes!");
            Debug.Log("Player takes second damage successfully");

            yield return null;
        }
    }
}
