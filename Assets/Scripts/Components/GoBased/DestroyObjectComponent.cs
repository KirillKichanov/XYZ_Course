using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Components.GoBased
{ 
    public class DestroyObjectComponent : MonoBehaviour

    { 
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}