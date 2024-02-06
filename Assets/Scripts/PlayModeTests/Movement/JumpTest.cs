using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class JumpTest : InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator JumpTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var startPosition = player.transform.position.y;
            var newJumpPosition = startPosition;
            var keyboard = InputSystem.AddDevice<Keyboard>();
            
            Press(keyboard.spaceKey);

            while (startPosition == newJumpPosition)
            {
                yield return null;
                newJumpPosition = player.transform.position.y;
            }
            
            newJumpPosition = player.transform.position.y;
            
            Assert.That(newJumpPosition > startPosition, "Player can't jump!");
            Debug.Log("Player jump successful!");
        }

        [UnityTest]
        public IEnumerator DoubleJumpTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var startPosition = player.transform.position.y;
            var firstJumpPosition = startPosition;
            var secondJumpPosition = startPosition;
            var keyboard = InputSystem.AddDevice<Keyboard>();
            
            Press(keyboard.spaceKey);

            while (startPosition == firstJumpPosition)
            {
                yield return null;
                firstJumpPosition = player.transform.position.y;
            }
            
            firstJumpPosition = player.transform.position.y;
            secondJumpPosition = firstJumpPosition;
            
            Release(keyboard.spaceKey);
            Press(keyboard.spaceKey);
            
            while (firstJumpPosition == secondJumpPosition)
            {
                yield return null;
                secondJumpPosition = player.transform.position.y;
            }
            
            secondJumpPosition = player.transform.position.y;
            
            Assert.That(firstJumpPosition > startPosition && secondJumpPosition > firstJumpPosition, "Player can't double jump!");
            Debug.Log("Player double jump successful!");
        }
    }
}
