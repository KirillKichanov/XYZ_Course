using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class CollectCoinsTest : InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("TestScene");
        }
        
        [UnityTest]
        public IEnumerator CollectSilverCoinsTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var silverCoin = GameObject.Find("SilverCoin");
            var startCoins = player.GetComponent<Hero>().Coins;

            player.transform.position = silverCoin.transform.position;
            
            yield return new WaitForFixedUpdate();

            var collectedCoins = player.GetComponent<Hero>().Coins;
            
            Assert.That(startCoins == 0 && collectedCoins == 1, "Player can't collect silver coins!");
            Debug.Log($"Player collected silver coin! Number of coins: {collectedCoins}");
        }
        
        [UnityTest]
        public IEnumerator CollectGoldenCoinsTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var goldenCoin = GameObject.Find("GoldenCoin");
            var startCoins = player.GetComponent<Hero>().Coins;
            
            player.transform.position = goldenCoin.transform.position;
            
            yield return new WaitForFixedUpdate();

            var collectedCoins = player.GetComponent<Hero>().Coins;
            
            Assert.That(startCoins == 0 && collectedCoins == 10, "Player can't collect coins!");
            Debug.Log($"Player collected golden coin! Number of coins: {collectedCoins}");
        }
    }
}
