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
        public GameObject _player;
        public GameObject _teleport;
        public GameObject _teleportDestination;
        
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator TeleportTestWithEnumeratorPasses()
        {
            _player = GameObject.Find("Hero");
            _teleport = GameObject.Find("Teleport");
            _teleportDestination = GameObject.Find("TeleportDestination1");

            var teleportPosition = _teleport.transform.position;
            var teleportDestinationPosition = _teleportDestination.transform.position;

            yield return null;
            
            _player.transform.position = teleportPosition;

            yield return new WaitUntil(() => _player.transform.position == teleportDestinationPosition);
            
            Assert.That(_player.transform.position == teleportDestinationPosition, "Player can't use teleport!");
            Debug.Log("Player teleported successfully!");
        }
    }
}
