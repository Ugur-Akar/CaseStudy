using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Settings

    // Connections

    public GameObject startCanvas;
    public GameObject ingameCanvas;
    public GameObject finishCanvas;
    public GameObject failCanvas;

    [Header("IngameCanvas")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public Action OnStartButtonPressed;
    public Action OnNextButtonPressed;
    public Action OnRetryButtonPressed;

    // State Variables
    
    // Start is called before the first frame update
    void Awake()
    {
        //InitConnections();
        InitState();
    }
    void InitConnections()
    {

    }
    void InitState()
    {
        startCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
        finishCanvas.SetActive(false);
        failCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonPressed()
    {
        OnStartButtonPressed?.Invoke();
        startCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
    }

    public void NextButtonPressed()
    {
        OnNextButtonPressed?.Invoke();
    }

    public void RetryButtonPressed()
    {
        OnRetryButtonPressed?.Invoke();
    }

    public void Fail()
    {
        ingameCanvas.SetActive(false);
        failCanvas.SetActive(true);
    }

    public void Success()
    {
        ingameCanvas.SetActive(false);
        finishCanvas.SetActive(true);
    }

    public void UpdateScoreUI(int score, int perfectScore)
    {
        scoreText.text = score + "/" + perfectScore; 
    }

    public void UpdateTimer(int min, int sec)
    {
        timerText.text = min + ":" + sec;
    }
}
