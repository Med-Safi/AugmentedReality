using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnHeight = 1.5f; // Adjust based on your cube height
    public int numberOfCoins = 10; // Total coins to spawn

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        // Assuming a simple 10x10 area for spawning coins
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), spawnHeight, Random.Range(-5f, 5f));
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
