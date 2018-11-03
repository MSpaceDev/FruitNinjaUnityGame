using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static GameManager instance;

    [Header("Sounds")]
    public AudioClip[] sounds;
    private AudioSource audioSource;

    [Header("In-Game Control")]
    public int gameTimeInSeconds = 120;
    private int gameTime;
    public Text gameTimeText;

    [Header("Scores")]
    public Text scoreText;
    public Text highscoreText;
    private int score;
    private int highscore;

    [Header("Game Over")]
    public Text gameOverScore;
    public Text gameOverBest;
    public GameObject gameOverPanel;

    private Coroutine timer;
    private int adCounter;


    private void Awake()
    {
        // Singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Advertisement.Initialize("2880755");

        audioSource = GetComponent<AudioSource>();

        gameOverPanel.SetActive(false);
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Best: " + highscore.ToString();

        gameTime = gameTimeInSeconds;
        timer = StartCoroutine(Timer());
    }

    public void Restart()
    {
        gameOverPanel.SetActive(false);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(go);
        }

        Time.timeScale = 1;
        gameTime = gameTimeInSeconds;
        timer = StartCoroutine(Timer());
    }

    public void GameOver()
    {
        StopCoroutine(timer);

        Time.timeScale = 0;
        scoreText.text = "0";

        gameOverScore.text = "Score: " + score.ToString();
        gameOverBest.text = "Best: " + highscore.ToString();
        gameOverPanel.SetActive(true);

        score = 0;

        // Show advert every 5 deaths
        adCounter++;
        if (adCounter % 5 == 0)
            Advertisement.Show();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        if (score > highscore)
            highscore = score;

        highscoreText.text = "Best: " + highscore.ToString();
        PlayerPrefs.SetInt("Highscore", highscore);
    }

    private IEnumerator Timer()
    {
        while(true)
        {
            System.TimeSpan span = System.TimeSpan.FromSeconds(gameTime);
            gameTimeText.text = string.Format("{0}:{1:00}", (int) span.TotalMinutes, span.Seconds);
            gameTime--;

            if (gameTime < 0)
                GameOver();

            yield return new WaitForSeconds(1);
        }
    }

    public void PlaySliceSound()
    {
        audioSource.pitch = Random.Range(0.5f, 0.9f);
        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }
}
