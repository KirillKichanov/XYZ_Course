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
        public IEnumerator ApplyHealWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var heroComponent = player.GetComponent<Hero>();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            
            yield return new WaitForSeconds(1);
            
            healthComponent.ApplyDamage(1);
            heroComponent.TakeDamage();
            var damagedHealth = healthComponent.health;
            healthComponent.ApplyHeal(1);
            var heal = healthComponent.health;
            
            yield return new WaitForSeconds(1);

            Assert.That(heal > damagedHealth || heal == startHealth, "Player can't heal!");
            Debug.Log("Player's heal successful!");

            yield return null;
        }

        [UnityTest]
        public IEnumerator PotionHealWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var heroComponent = player.GetComponent<Hero>();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.health;
            var potion = GameObject.Find("HealingPotion").transform.position;
            
            yield return new WaitForSeconds(1);

            healthComponent.ApplyDamage(5);
            heroComponent.TakeDamage();
            var damagedHealth = healthComponent.health;
            
            yield return new WaitForSeconds(1);
            
            player.transform.position = potion;
            
            yield return new WaitForSeconds(1);
            
            var potionHeal = healthComponent.health;
            Assert.That(potionHeal > damagedHealth || potionHeal == startHealth, "Player can't heal by potion!");
            Debug.Log("Player's heal by potion successful!");
        }
    }
}
