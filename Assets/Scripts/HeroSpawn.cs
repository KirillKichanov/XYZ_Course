using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class HeroSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _teleportPrefab;
    [SerializeField] private GameObject _heroPrefab;
    [SerializeField] private HeroCameraFollow _heroCameraFollow;
    void Start()
    {
        StartCoroutine(SpawnTeleportPrefab());
    }

    public void SpawnHeroPrefab()
    {
        var hero = Instantiate(_heroPrefab, transform.position, Quaternion.identity);
        GameManager.Instance.Hero = hero;
        _heroCameraFollow.SetCameraFollow(hero.transform);
    }

    public IEnumerator SpawnTeleportPrefab()
    {
        var teleport = Instantiate(_teleportPrefab, transform.position, Quaternion.identity);
        _heroCameraFollow.SetCameraFollow(teleport.transform);
        yield return new WaitForSeconds(1);
        SpawnHeroPrefab();
    }
}
