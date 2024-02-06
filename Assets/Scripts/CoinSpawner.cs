using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnHeight = 1.5f; 
    public int numberOfCoins = 10; 

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        //  1x1 area for spawning coins
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-1f, 1f), spawnHeight, Random.Range(-5f, 5f));
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
