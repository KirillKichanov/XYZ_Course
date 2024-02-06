using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class MovementTest : InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator TestPlayerMovementLeftWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var startPosition = player.transform.position.x;
            var newPosition = startPosition;
            
            var keyboard = InputSystem.AddDevice<Keyboard>();
            
            Press(keyboard.aKey);

            while (startPosition == newPosition)
            {
                yield return null;
                newPosition = player.transform.position.x;
            }
            
            newPosition = player.transform.position.x;
            
            Assert.That(startPosition > newPosition, "Player can't move left!");
            Debug.Log("Player move left successful!");
        }

        [UnityTest]
        public IEnumerator TestPlayerMovementRightWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var startPosition = player.transform.position.x;
            var newPosition = startPosition;
            
            var keyboard = InputSystem.AddDevice<Keyboard>();
            
            Press(keyboard.dKey);

            while (startPosition == newPosition)
            {
                yield return null;
                newPosition = player.transform.position.x;
            }
            
            newPosition = player.transform.position.x;
            
            Assert.That(startPosition < newPosition, "Player can't move right!");
            Debug.Log("Player move right successful!");
        }

        //нужно для очистки сцены, если что-то создавалось во время теста
        public override void TearDown()
        {
            base.TearDown();
        }
    }
}
