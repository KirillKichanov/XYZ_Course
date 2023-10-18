using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace Tests
{
    public class HeroInitializationTest
    {
        [Test]
        public void HeroInitializationTestSimplePasses()
        {
            var playerGameObject = GameObject.Find("Hero_0");
            Assert.That(playerGameObject, Is.Not.Null);
            Debug.Log("Hero +");
            
            var playerInputReader = playerGameObject.GetComponent<HeroInputReader>();
            Assert.That(playerInputReader, Is.Not.Null);
            Debug.Log("Input Reader +");
            
            var playerInputSystem = playerGameObject.GetComponent<PlayerInput>();
            Assert.That(playerInputSystem, Is.Not.Null);
            Debug.Log("Input System +");

            var playerSpeed = playerGameObject.GetComponent<Hero>()._speed;
            Assert.That(playerSpeed == 3,"playerSpeed != 3");
            Debug.Log("Speed +");
        }
    }
}
