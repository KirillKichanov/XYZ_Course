using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private GameObject _hero;
    [SerializeField] private GameObject _startPortal;
    
    void Update()
    {
        if (_startPortal == null)
        {
            _hero.SetActive(true);
        }
    }
}
