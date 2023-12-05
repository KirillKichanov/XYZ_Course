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
    public class HealingTest: InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator HealingTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var heroComponent = player.GetComponent<Hero>();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            var potion = GameObject.Find("HealingPotion").transform.position;
            
            yield return new WaitForSeconds(1);
            
            healthComponent.ApplyDamage(6);
            heroComponent.TakeDamage();
            var damagedHealth = healthComponent.health;
            healthComponent.ApplyHeal(1);
            var firstHeal = healthComponent.health;

            Assert.That(firstHeal > damagedHealth, "Player can't heal!");
            Debug.Log("Player's first heal successful!");
            
            yield return new WaitForSeconds(1);
            
            player.transform.position = potion;
            
            yield return new WaitForSeconds(1);
            
            var secondHeal = healthComponent.health;
            Assert.That(startHealth == secondHeal, "Player can't heal by potion!");
            Debug.Log("Player's second heal successful!");

            yield return null;
        }
    }
}
