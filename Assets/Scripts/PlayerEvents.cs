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
    public Animator animator;
    public Rigidbody rb;
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
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        intro.Play();
        StartCoroutine(goScene());
    }

    private void Update()
    {

        if (playScene == true) {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                animator.SetBool("Eating", true);
            }
            else
            {
                animator.SetBool("Eating", false);
            }
        }

        if (score == scoreToWin)
        {
            
            labyrinthKey.SetActive(true);
            fruit.Play();
            labyrinthKey.transform.localScale = Vector3.zero;
            StartCoroutine(LabyrinthKeyAnimation());
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Enemy get you
        if (collision.gameObject.CompareTag("Ghost"))
        {
            StartCoroutine(DeathAnimation());
            Destroy(this);
            music.Stop();
            death.Play();
            resultText.gameObject.SetActive(true);
            resultText.text = gameOverText;
            animator.SetBool("Eating", false);
            animator.enabled = false;
            playScene = false;
        }

        //EnergyCells count
        if (collision.gameObject.CompareTag("EnergyCell"))
        {
            StartCoroutine(ScoreAnimation());
            score++;
            dots.Play();
            scoreText.text = score.ToString();
            Destroy(collision.gameObject);
        }

        //Key Reward - End of the game
        if (collision.gameObject.CompareTag("LabyrinthKey"))
        {
            Destroy(labyrinthKey);
            StartCoroutine(WinAnimation());
            resultText.gameObject.SetActive(true);
            resultText.text = gameWinnerText;
            music.Stop();
            win.Play();
            animator.SetBool("Eating", false);
            playScene = false;
        }

        //Teleports
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
            labyrinthKey.transform.LeanScale(Vector3.one * 5f, 0.5f).setEaseInOutBack();
            yield return new WaitForSeconds(0.1f);
            labyrinthKey.transform.LeanScale(Vector3.one, 0.5f).setEaseInOutBack();
            yield break;
        }
    }

    private IEnumerator DeathAnimation()
    {
        while (true)
        {
            transform.LeanRotateX(-360, 2f);
            transform.LeanRotateY(-360, 2f);
            transform.LeanRotateZ(-360, 2f);
            transform.LeanScale(Vector3.one * 5f, 0.5f);
            yield break;
        }
    }

    private IEnumerator WinAnimation()
    {
        while (true)
        {
            transform.LeanRotateY(140, 1f).setEaseInOutBack();
            yield return new WaitForSeconds(2f);
            transform.LeanRotateY(-140, 1f).setEaseInOutBack();
            yield return new WaitForSeconds(2f);
            yield return null;
        }
    }

    private IEnumerator goScene()
    {
        yield return new WaitForSeconds(4f);
        playScene = true;
        music.Play();
        yield return null;
    }
}
