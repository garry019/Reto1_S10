using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerEvents : MonoBehaviour
{
    public bool playScene = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] public GameObject labyrinthKey;
    [SerializeField] public AudioSource intro;
    [SerializeField] public AudioSource music;
    [SerializeField] public AudioSource win;
    [SerializeField] public AudioSource dots;
    [SerializeField] public AudioSource death;
    [SerializeField] public AudioSource teleport;
    [SerializeField] public AudioSource fruit;

    private int score = 0;
    private int scoreToWin = 15;
    public string gameOverText = "Game Over";
    public string gameWinnerText = "Youn have Win!";


    private void Start()
    {
        intro.Play();
        StartCoroutine(goScene());
    }

    private void Update()
    {
        if (score == scoreToWin)
        {
            labyrinthKey.transform.localScale = Vector3.zero;
            labyrinthKey.SetActive(true);
            fruit.Play();
            StartCoroutine(LabyrinthKeyAnimation());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Destroy(this);
            music.Stop();
            death.Play();
            resultText.text = gameOverText;
            playScene = false;

        }

        if (collision.gameObject.CompareTag("EnergyCell"))
        {
            StartCoroutine(ScoreAnimation());
            score++;
            dots.Play();
            scoreText.text = score.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("LabyrinthKey"))
        {
            resultText.text = gameWinnerText;
            music.Stop();
            win.Play();
            playScene = false;
        }

        if (collision.gameObject.CompareTag("LeftCollision"))
        {
            transform.position = new Vector2(10f, 0);
            teleport.Play();
        }

        if (collision.gameObject.CompareTag("RightCollision"))
        {
            transform.position = new Vector2(-10f, 0);
            teleport.Play();
        }
    }

    private IEnumerator ScoreAnimation()
    {
        while (true)
        {
            scoreText.transform.LeanScale(Vector2.one * 2f, 0.1f).setEaseInOutBack();
            yield return new WaitForSeconds(0.1f);
            scoreText.transform.LeanScale(Vector2.one, 0.1f).setEaseInOutBack();
            yield break;
        }
    }
    private IEnumerator LabyrinthKeyAnimation()
    {
        while (true)
        {
            labyrinthKey.transform.LeanScale(Vector3.one * 40f, 0.5f).setEaseInOutBack();
            yield return new WaitForSeconds(0.1f);
            labyrinthKey.transform.LeanScale(Vector3.one, 0.5f).setEaseInOutBack();
            yield break;
        }
    }

    private IEnumerator goScene()
    {
        yield return new WaitForSeconds(3.5f);
        playScene = true;
    }
}
