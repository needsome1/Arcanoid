using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
   [SerializeField] private TMP_Text livesText;
   [SerializeField] private TMP_Text scoreText;
   [SerializeField] private TMP_Text hiScoreText;
   [SerializeField] private int startLives = 5;
   [SerializeField] private GameObject blackScreen;
   [SerializeField] private GameObject gameWinText;
   [SerializeField] private GameObject gameOverText;
   [SerializeField] private GameObject restartButton;
   
   
   
   private int currentLives;
   private int currentScore;
   private int currentHiscore;

   private void Awake()
   {
      int instanceCount = FindObjectsOfType<GameSession>().Length;
      if (instanceCount>1)
      {
         Destroy(gameObject);
      }
      else
      {
         DontDestroyOnLoad(gameObject);
      }
   }

   private void Start()
   {
      currentLives = startLives;
      
      restartButton.GetComponent<Button>().onClick.AddListener(Reset);

      currentHiscore = PlayerPrefs.GetInt("highscore");

   }

   private void Update()
   {
      CountBalls();

      CountBlocks();

      UpdateUI();
   }
   public void IncreaseScore(int value)
   {
      currentScore += value; 
   }

   public void DecreaseLives()
   {
      currentLives--;
      if (currentLives<=0) 
      {
         GameOver(); 
      }
   }

   private void CountBalls()
   {
      var balls = GameObject.FindGameObjectsWithTag("Ball");
      if (balls.Length == 0)
      {
         DecreaseLives();
         GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ResetBall();
      }
   }

   private void CountBlocks()
   {
      var blocks = GameObject.FindGameObjectsWithTag("Block");
      {
         if (blocks.Length == 0)
         {
            Win();
         }
      }
   }

   private void UpdateUI()
   {
      livesText.text = currentLives.ToString();
      scoreText.text = currentScore.ToString();
      hiScoreText.text = currentHiscore.ToString();
   }



   private void Win()
   {
      var numberOfScenes = SceneManager.sceneCountInBuildSettings;
      var currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
      if (currentBuildIndex == numberOfScenes-1)
      {
         blackScreen.SetActive(true);
         restartButton.SetActive(true);
         gameWinText.SetActive(true);
         
         SetHighScore();
         
         Pause();
         
      }
      else
      {
         SceneManager.LoadScene(currentBuildIndex + 1);
      }
      
   }

   private void GameOver()
   {
      blackScreen.SetActive(true);
      gameOverText.SetActive(true);
      restartButton.SetActive(true);
      SetHighScore();
      Pause();
   }



   private void Reset()
   {
      ResetsScore();
      ResetScreen();
      ResetLevel1();
   }
   private void ResetScreen()
   {
      blackScreen.SetActive(false);
      gameOverText.SetActive(false);
      gameWinText.SetActive(false);
      restartButton.SetActive(false);
      
      UnPause();
   }

   private void ResetsScore()
   {
      currentLives = startLives;
      currentScore = 0;
   }

   private void ResetLevel1()
   {
      SceneManager.LoadScene(0);
   }

   private void SetHighScore()
   {
      if (currentScore>currentHiscore)
      {
         currentHiscore = currentScore;
         PlayerPrefs.SetInt("highscore",currentHiscore);
      }
   }

   private void Pause()
   {
      Time.timeScale = 0f;
   }
   private void UnPause()
   {
      Time.timeScale = 1f;
   }
}
