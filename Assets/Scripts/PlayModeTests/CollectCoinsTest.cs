using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Model;
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
            var startCoins = GameObject.Find("GameSession").GetComponent<GameSession>().Data.Coins;

            player.transform.position = silverCoin.transform.position;
            
            yield return new WaitForFixedUpdate();

            var collectedCoins = GameObject.Find("GameSession").GetComponent<GameSession>().Data.Coins;
            
            Assert.That(startCoins == 0 && collectedCoins == 1, $"Player can't collect silver coins! Number of coins: {collectedCoins}");
            Debug.Log($"Player collected silver coin! Number of coins: {collectedCoins}");
        }
        
        [UnityTest]
        public IEnumerator CollectGoldenCoinsTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var goldenCoin = GameObject.Find("GoldenCoin");
            var startCoins = GameObject.Find("GameSession").GetComponent<GameSession>().Data.Coins;
            
            player.transform.position = goldenCoin.transform.position;
            
            yield return new WaitForFixedUpdate();

            var collectedCoins = GameObject.Find("GameSession").GetComponent<GameSession>().Data.Coins;
            
            Assert.That(startCoins == 0 && collectedCoins == 10, $"Player can't collect coins! Number of coins: {collectedCoins}");
            Debug.Log($"Player collected golden coin! Number of coins: {collectedCoins}");
        }
    }
}
