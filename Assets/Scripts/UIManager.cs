using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI timerText;
    private int timeAlive;
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timeAlive++;
        timerText.text = "Time Alive: " + timeAlive;
    }

    public void updateHealth()
    {
        healthText.text = "Player Health: ";
    }
}
