using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class InteractTest : InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene(0);
        }

        [UnityTest]
        public IEnumerator InteractWithPlatformSwitchWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var platformSwitch = GameObject.Find("SwitchPlatform");
            var keyboard = InputSystem.AddDevice<Keyboard>();
            var platform = GameObject.Find("UpDownPlatform");
            
            yield return null;

            player.transform.position = platformSwitch.transform.position;
            
            yield return null;

            Press(keyboard.eKey);
            Release(keyboard.eKey);
            
            yield return new WaitForSeconds(1);
            
            var isPlatformActive = platform.GetComponent<CycledMovingComponent>().isPlatformActive;

            Assert.That(isPlatformActive, "Platform switch does not work!");
            Debug.Log("Platform switch works!");
        }

        [UnityTest]
        public IEnumerator InteractWithDoorSwitchWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var doorSwitch = GameObject.Find("SwitchDoor");
            var door = GameObject.Find("Door_0");
            var keyboard = InputSystem.AddDevice<Keyboard>();
            
            yield return null;

            player.transform.position = doorSwitch.transform.position;
            
            yield return null;
            
            Press(keyboard.eKey);
            Release(keyboard.eKey);
            
            yield return new WaitForSeconds(1);

            var doorState = door.GetComponent<SwitchComponent>().doorState;
            var doorCollider = door.GetComponent<BoxCollider2D>().enabled;
            
            Assert.That(doorState || doorCollider == false, "Door can't be opened!");
            Debug.Log("Door switch works!");
        }

        [UnityTest]
        public IEnumerator InteractWithDoorAndWindSwitchWithEnumeratorPasses()
        {
            var player = GameObject.Find("Hero");
            var doorAndWindSwitch = GameObject.Find("SwitchDoorAndWind");
            var door = GameObject.Find("Door_1");
            var wind = GameObject.Find("Wind");
            var keyboard = InputSystem.AddDevice<Keyboard>();
            
            yield return null;
            
            player.transform.position = doorAndWindSwitch.transform.position;

            yield return null;

            Press(keyboard.eKey);
            Release(keyboard.eKey);
            
            yield return new WaitForSeconds(1);

            var doorState = door.GetComponent<SwitchComponent>().doorState;
            var doorCollider = door.GetComponent<BoxCollider2D>().enabled;
            var windState = wind.GetComponent<WindComponent>().activeWind.enabled;
            
            Assert.That(doorState || doorCollider == false || windState, "Wind can't be activated!");
            Debug.Log("Door and wind switch works!");
        }
    }
}
