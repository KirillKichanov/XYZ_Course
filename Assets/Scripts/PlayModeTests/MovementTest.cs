using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovementTest
    {
        [SerializeField] private GameObject player;
        
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void TestPlayerMovement()
        {
        }
        
        [UnityTest]
        public IEnumerator MovementTestWithEnumeratorPasses()
        {
            yield return null;
        }
    }
}
