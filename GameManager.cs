using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI finalScore;
    public GameObject endScene;
    public GameObject gameOverText;
    public GameObject titleScreen;
    public GameObject restartButton;
    public GameObject quitButton;

    private int score;
    private int highScore = 0;
    private int storeScore;
    public bool isGameActive;
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
       highScoreText.text = "HighScore: " + HighScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame(float difficulty)
    {
        spawnRate /= difficulty; 
        titleScreen.SetActive(false);
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTargets());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreIncreament)
    {
        if(isGameActive == true)
        {
            score += scoreIncreament;

            scoreText.text = "Score: " + score;
            finalScore.text = "Your Score: " + score;
            PlayerPrefs.SetInt("storeScore", score);
        }
     
    }

    int HighScore()
    {
        if(PlayerPrefs.GetInt("storeScore") >= PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("storeScore"));
        }
        return PlayerPrefs.GetInt("highScore");
    }

    public void GameOver()
    {
        isGameActive = false;
        endScene.SetActive(true);
        scoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
    }

    IEnumerator SpawnTargets()
    {
        while (isGameActive == true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }

    }
}
