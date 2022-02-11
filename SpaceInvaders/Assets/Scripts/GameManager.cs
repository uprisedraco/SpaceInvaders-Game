using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private int playerHealth = 3;

    [SerializeField]
    private int playerScore = 0;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private int timer = 3;

    [SerializeField]
    private GameObject endGameMenu;

    [SerializeField]
    private Text endGameText;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private Vector3 playerLocation = new Vector3(0f,0f,-40f);

    [SerializeField]
    private GameObject enemiesPrefab;

    [SerializeField]
    private Vector3 enemiesLocation = new Vector3(0f,0f,40f);

    private void Awake()
    {
        instance = this;
        livesText.text = playerHealth.ToString();
        scoreText.text = playerScore.ToString();
        StartCoroutine("Timer");
        LocatePlayer();
        LocateEnemies();
    }

    private void LocatePlayer()
    {
        Instantiate(playerPrefab, playerLocation, Quaternion.identity);
    }

    private void LocateEnemies()
    {
        Instantiate(enemiesPrefab, enemiesLocation, Quaternion.identity);
    }

    public void UpdateScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString();
    }

    public void RemoveHealth()
    {
        playerHealth--;
        livesText.text = playerHealth.ToString();
        if (playerHealth <= 0)
        {
            LoseScreen();
        }
        else
        {
            StartCoroutine("Timer");
            LocatePlayer();
        }
    }

    public void WinScreen()
    {
        Time.timeScale = 0;
        endGameText.text = "You WIN";
        endGameMenu.SetActive(true);
    }

    public void LoseScreen()
    {
        Time.timeScale = 0;
        endGameText.text = "You LOSE";
        endGameMenu.SetActive(true);
    }

    IEnumerator Timer()
    {
        Time.timeScale = 0;
        timerText.gameObject.SetActive(true);
        for (int i = timer; i > 0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        timerText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
