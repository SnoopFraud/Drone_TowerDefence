using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject scoreUI;

    public bool isGameOver, succeed;
    public GameObject gameOverScreen;
    public GameObject succeedscreen;

    public string wavecount;
    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI wavetxt;
    public GoalCollision goal;
    public EnemySpawner wave;

    private void Awake()
    {
        goal = GameObject.FindGameObjectWithTag("Tujuan").GetComponent<GoalCollision>();
        wave = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();

        scoreUI.SetActive(true);

        Time.timeScale = 1;
        //GameOver Screen
        isGameOver = false;
        gameOverScreen.SetActive(false);
        //Success screen
        succeed = false;
        succeedscreen.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoretxt.text = goal.health.ToString();
        wavetxt.text = wavecount;

        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            scoreUI.SetActive(false);
        }
        if (succeed)
        {
            succeedscreen.SetActive(true);
            Time.timeScale = 0;
            scoreUI.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
