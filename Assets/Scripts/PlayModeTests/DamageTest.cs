using System.Collections;
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
        public IEnumerator ApplyDamageWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var heroComponent = player.GetComponent<Hero>();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            
            yield return new WaitForSeconds(1);

            healthComponent.ApplyDamage(1);
            heroComponent.TakeDamage();
            var damage = healthComponent.health;
            Assert.That(startHealth > damage, "Player can't take damage!");
            Debug.Log("Player takes damage successfully");

            yield return null;
        }

        [UnityTest]
        public IEnumerator SpikesDamageWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            var spikes = GameObject.Find("Spikes (1)").transform.position;
            
            player.transform.position = spikes;
            
            yield return new WaitForSeconds(1);

            var spikesDamage = healthComponent.health;
            Assert.That(startHealth > spikesDamage, "Player can't take damage by spikes!");
            Debug.Log("Player takes damage by spikes successfully");

            yield return null;
        }
    }
}
