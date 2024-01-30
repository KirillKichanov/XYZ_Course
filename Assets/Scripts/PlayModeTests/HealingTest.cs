using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Components;
using Scripts.Components.Health;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Scripts.Creatures;
using Scripts.Creatures.Hero;

namespace Tests
{
    public class HealingTest: InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator PotionHealWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var heroComponent = player.GetComponent<Hero>();
            var healthComponent = player.GetComponent<HealthComponent>();
            var startHealth = healthComponent.Health;
            var potion = GameObject.Find("HealingPotion").transform.position;
            
            healthComponent.ApplyDamage(5);
            heroComponent.TakeDamage();
            var damagedHealth = healthComponent.Health;
            
            player.transform.position = potion;
            
            yield return new WaitForFixedUpdate();
            
            var potionHeal = healthComponent.Health;
            Assert.That(potionHeal > damagedHealth && potionHeal == startHealth, "Player can't heal by potion!");
            Debug.Log("Player's heal by potion successful!");
        }
    }
}
