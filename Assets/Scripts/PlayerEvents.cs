using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public GameObject labyrinthKey;

    private int score = 0;
    public string gameOverText = "Game Over";

    private void Update()
    {
        if (score == 139)
        {
            labyrinthKey.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Destroy(this);
            scoreText.text = gameOverText;
            PauseGame();
        }

        if (collision.gameObject.CompareTag("EnergyCell"))
        {
            score++;
            scoreText.text = score.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("LabyrinthKey"))
        {
            scoreText.text = "Win!!";
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
}
