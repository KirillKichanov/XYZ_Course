using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Components
{ 
    public class DestroyObjectComponent : MonoBehaviour

    { 
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}