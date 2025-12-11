using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //private TextMeshProUGUI healthText;
    //private TextMeshProUGUI timerText;
    // Blackboard position status
    [SerializeField] [CanBeNull] private TMP_Text playerPosition;
    
    [SerializeField] [CanBeNull] private RectTransform healthBar;
        
    [SerializeField] private float maxHealth;
    private float currentHealth = 20f;

    [SerializeField] private float width, height;
    
    
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
        //timerText.text = "Time Alive: " + timeAlive;
        if (transform.TryGetComponent(out GameManager gameManager))
        {
            var position = transform.GetComponent<GameManager>().getBlackboard().getPlayerPosition();
            var posText = "("+position.x + "," + position.y + "," + position.z + ")";
            playerPosition.text = posText;
        }
    }

    public void setMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void setHealth(float health)
    {
        currentHealth = health;
            
        float newWidth = ((maxHealth - currentHealth) / maxHealth) * width;
            
        healthBar.sizeDelta = new Vector2(newWidth, height);
    }
}
