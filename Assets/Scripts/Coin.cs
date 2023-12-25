using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _amount;

    public void Collect(GameObject go)
    {
        Hero hero = go.GetComponent<Hero>();
        if (!hero) return;
    
        hero.CoinCollect(_amount);
    }
}
