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
        public GameObject _player;
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator CollectCoinsTestWithEnumeratorPasses()
        {
            _player = GameObject.Find("Hero");
            var keyboard = InputSystem.AddDevice<Keyboard>();
            
            Press(keyboard.dKey);
            yield return new WaitForSeconds(1);

            var hero = _player.GetComponent<Hero>();
            var coins = ReflectionHelper.GetPrivateFieldValue<int>(hero, "_coins");
            
            Assert.That(coins == 11, "Player can't collect coins!");
            Debug.Log("Player can collect coins!");
        }
    }
}
