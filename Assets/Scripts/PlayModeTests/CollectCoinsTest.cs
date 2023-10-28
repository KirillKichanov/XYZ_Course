using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class CollectCoinsTest : InputTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator CollectCoinsTestWithEnumeratorPasses()
        {
            yield return null;
        }
    }
}
