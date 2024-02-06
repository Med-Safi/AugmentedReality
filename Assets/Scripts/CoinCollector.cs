using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollector : MonoBehaviour
{
    public static int score = 0; // Static score accessible from anywhere

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject); // Destroy the coin
            score++; // Increase score
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        // Assuming you have a UI Text element assigned to display the score
        ScoreManager.Instance.UpdateScoreText(score);
    }
}
