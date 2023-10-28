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
            SceneManager.LoadScene(0);
        }

        [UnityTest]
        public IEnumerator JumpTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var startPosition = player.transform.position.y;
            
            //jump test
            var keyboard = InputSystem.AddDevice<Keyboard>();
            Press(keyboard.spaceKey);
            yield return new WaitForSeconds(0.1f);
            Release(keyboard.spaceKey);

            var newJumpPosition = player.transform.position.y;
            Assert.That(newJumpPosition > startPosition, "Player can't jump!");
            Debug.Log("Player jump successful!");
            yield return new WaitForSeconds(0.1f);
            
            //double jump test
            Press(keyboard.spaceKey);
            yield return new WaitForSeconds(0.3f);
            Release(keyboard.spaceKey);

            var newDoubleJumpPosition = player.transform.position.y;
            Assert.That(newDoubleJumpPosition > newJumpPosition, "Player can't do double jump!");
            Debug.Log("Player double jump successful!");
        }
    }
}
