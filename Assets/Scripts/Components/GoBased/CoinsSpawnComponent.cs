using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Components.GoBased
{
    public class CoinsSpawnComponent : MonoBehaviour
    {
        [SerializeField] private int _coinsToSpawn;
        [SerializeField] private float _spawnRadius;
        
        [SerializeField] private GameObject _silverCoin;
        [SerializeField] private GameObject _goldenCoin;

        [SerializeField] private float _goldenProbability;

        public void CoinsSpawn()
        {
            for (int i = 0; i < _coinsToSpawn; i++)
            {
                Vector3 randomPosition = transform.position + new Vector3(Random.Range(-_spawnRadius, _spawnRadius), 
                    Random.Range(-_spawnRadius, _spawnRadius), 0f);
                float randomValue = Random.value;
                if (randomValue <= _goldenProbability)
                {
                    Instantiate(_goldenCoin, randomPosition, Quaternion.identity);
                }
                else if (randomValue > _goldenProbability)
                {
                    Instantiate(_silverCoin, randomPosition, Quaternion.identity);
                }
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _spawnRadius);
        }
    }
}
