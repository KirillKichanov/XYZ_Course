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
        public IEnumerator DamageTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var heroComponent = player.GetComponent<Hero>();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            var playerLocation = player.transform.position;
            var spikes = GameObject.Find("Spikes (1)").transform.position;
            
            yield return new WaitForSeconds(1);

            healthComponent.ApplyDamage(1);
            heroComponent.TakeDamage();
            //yield return null;

            //player.transform.position = spikes;
            Debug.Log(healthComponent.health);

            yield return new WaitForSeconds(1);

            
            yield return null;
        }
    }
}
