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
            SceneManager.LoadScene(0);
        }

        [UnityTest]
        public IEnumerator TestPlayerMovement()
        {
            var player = GameObject.Find("Hero");
            var startPosition = player.transform.position.x;
            
            //move left test
            var keyboard = InputSystem.AddDevice<Keyboard>();
            Press(keyboard.aKey);
            yield return new WaitForSeconds(1);
            
            Release(keyboard.aKey);
            var newPosition = player.transform.position.x;
            
            Assert.That(startPosition > newPosition, "Player can't move left!");
            Debug.Log("Player move left successful!");

            yield return new WaitForSeconds(1);
            
            //move right test
            startPosition = player.transform.position.x;
            Press(keyboard.dKey);
            yield return new WaitForSeconds(1);
            
            Release(keyboard.dKey);
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
