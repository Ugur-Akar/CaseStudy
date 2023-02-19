using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Settings
    public float successPercent = 0.5f;
    // Connections
    public UIManager ui; 
    public List<GameObject> levels;

    LevelManager levelManager;
    // State Variables
    public int levelIndex = 0;

    public int score = 0;
    int perfectScore = 0;

    bool isPlaying = false;
    float timer = 0;
    // Start is called before the first frame update
    void Awake()
    {
        InitState();
        InstantiateLevel();
        InitConnections();
    }
    void InitConnections()
    {
        EventManager.RightArrival += RightArrival;
        EventManager.WrongArrival += WrongArrival;
        EventManager.AllTrainsArrived += AllTrainsArrived;

        //UI
        ui.OnStartButtonPressed += OnStartButtonPressed;
        ui.OnNextButtonPressed += OnNextButtonPressed;
    }
    void InitState()
    {
        levelIndex = PlayerPrefs.GetInt("levelIndex", 0);
        if(levelIndex >= levels.Count)
        {
            levelIndex %= levels.Count;
        }
    }

    void InstantiateLevel()
    {
        GameObject levelGO = Instantiate(levels[levelIndex]);
        levelManager = levelGO.GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                timer = 0;
                levelManager.levelDuration--;
                if(levelManager.levelDuration >= 0)
                {
                    int sec = levelManager.levelDuration % 60;
                    int min = levelManager.levelDuration / 60;
                    ui.UpdateTimer(min, sec);
                }
                else
                {
                    levelManager.StopSpawning();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnNextButtonPressed();
        }
    }


    void OnStartButtonPressed()
    {
        levelManager.OnStartButtonPressed();
        isPlaying = true;
        int sec = levelManager.levelDuration % 60;
        int min = levelManager.levelDuration / 60;
        ui.UpdateTimer(min, sec);
    }

    void OnNextButtonPressed()
    {
        levelIndex++;
        PlayerPrefs.SetInt("levelIndex", levelIndex);
        EventManager.CleanEvents();
        SceneManager.LoadScene(0);
    }

    void OnRetryButtonPressed()
    {
        EventManager.CleanEvents();
        SceneManager.LoadScene(0);
    }

    void RightArrival(Train train)
    {
        score++;
        perfectScore++;
        ui.UpdateScoreUI(score, perfectScore);
    }

    void WrongArrival(Train train)
    {
        perfectScore++;
        ui.UpdateScoreUI(score, perfectScore);
    }

    void AllTrainsArrived()
    {
        if(score <= perfectScore * successPercent)
        {
            ui.Fail();
        }
        else
        {
            ui.Success();
        }
    }
}
