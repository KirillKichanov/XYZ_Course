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
    public class TakingDamageTest: InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator SpikesDamageWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            var spikes = GameObject.Find("Spikes").transform.position;
            
            player.transform.position = spikes;
            
            yield return new WaitForFixedUpdate();

            var spikesDamage = healthComponent.health;
            Assert.That(startHealth > spikesDamage, "Player can't take damage by spikes!");
            Debug.Log("Player takes damage by spikes successfully");
        }
    }
}
