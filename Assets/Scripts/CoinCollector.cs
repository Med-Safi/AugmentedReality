using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollector : MonoBehaviour
{
    public static int score = 0;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject); 
            score++; 
            UpdateScoreUI();
            audioSource.Play();
        }
    }

    void UpdateScoreUI()
    {
        ScoreManager.Instance.UpdateScoreText(score);
    }
}
