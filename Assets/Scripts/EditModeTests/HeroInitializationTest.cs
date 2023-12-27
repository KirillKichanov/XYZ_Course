using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Scripts.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.TestTools;
using Scripts.Creatures;

namespace Tests
{
    public class HeroInitializationTest
    {
        [Test]
        public void HeroInitializationTestSimplePasses()
        {
            var playerGameObject = GameObject.Find("Hero");
            Assert.That(playerGameObject, Is.Not.Null, "There is no Hero on scene");
            Debug.Log("Hero on scene");
            
            var playerSprite = playerGameObject.GetComponent<SpriteRenderer>();
            Assert.That(playerSprite, Is.Not.Null, "Hero has no sprite");
            Debug.Log("Hero has sprite");
            
            var playerRigidbody = playerGameObject.GetComponent<Rigidbody2D>();
            Assert.That(playerRigidbody, Is.Not.Null, "Hero Rigidbody is not connected");
            Debug.Log("Hero Rigidbody is connected");
            
            var playerCollider = playerGameObject.GetComponent<CapsuleCollider2D>();
            Assert.That(playerCollider, Is.Not.Null, "Hero Input Reader is not connected");
            Debug.Log("Input Reader connected");
            
            var playerMainScript = playerGameObject.GetComponent<Hero>();
            Assert.That(playerMainScript, Is.Not.Null, "Hero main script is not connected");
            Debug.Log("Hero main script is connected");
            
            var playerInputReader = playerGameObject.GetComponent<HeroInputReader>();
            Assert.That(playerInputReader, Is.Not.Null, "Hero Input Reader is not connected");
            Debug.Log("Input Reader connected");
            
            var playerInputSystem = playerGameObject.GetComponent<PlayerInput>();
            Assert.That(playerInputSystem, Is.Not.Null, "Input System is not connected");
            Debug.Log("Input System connected");
            
            var hero = playerGameObject.GetComponent<Hero>();
            var jumpForce = ReflectionHelper.GetPrivateFieldValue<float>(hero, "_jumpForce");
            Assert.That(jumpForce == 10,"Player jump force not equals 10");
            Debug.Log("Correct player jump force");
            
            //var speed = hero.Speed;
            //Assert.That(speed == 3,"Player speed not equals 3");
            //Debug.Log("Correct player speed");

            var animator = playerGameObject.GetComponent<Animator>();
            Assert.That(animator, Is.Not.Null, "No Animator component");
            Debug.Log("Animator component connected");

        }
    }
}
