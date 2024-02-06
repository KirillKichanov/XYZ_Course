using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TeleportTest : InputTestFixture
    {
        
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("TestScene");
        }
        
        [UnityTest]
        public IEnumerator TeleportTestWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var teleport = GameObject.Find("Teleport");
            var teleportDestination = GameObject.Find("TeleportDestination1");

            var teleportPosition = teleport.transform.position;
            var teleportDestinationPosition = teleportDestination.transform.position;

            player.transform.position = teleportPosition;
            
            yield return new WaitUntil(() => player.transform.position == teleportDestinationPosition);
            
            Assert.That(player.transform.position == teleportDestinationPosition, "Player can't use teleport!");
            Debug.Log("Player teleported successfully!");
        }
    }
}
