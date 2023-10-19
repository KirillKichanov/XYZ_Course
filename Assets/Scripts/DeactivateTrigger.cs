using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WorldBorder")
        {
            Destroy(gameObject);
        }
    }
}
