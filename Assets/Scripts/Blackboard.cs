using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    [SerializeField]
    private Vector3 playerPosition;
    private Vector3 lastPlayerPosition;
    private static Blackboard blackboard;
    private float health;
    private List<GameObject> enemies =  new List<GameObject>();
    private UIManager uIManager;
    
    private Blackboard() {}

    public static Blackboard instance
    {
        get
        {
            if (blackboard == null)
            {
                blackboard = new Blackboard();
            }
            return blackboard;
        }
    }

    public void setPlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }

    public void setHealth(float health)
    {
        this.health = health;
    }

    public static Blackboard getInstance()
    {
        if (blackboard == null)
            blackboard = new Blackboard();
        return blackboard;
    }

    public float getHealth()
    {
        return health;
    }

    public Vector3 getPlayerPosition()
    {
        return playerPosition;
    }

    public GameObject getEnemyPosition(int index)
    {
        return enemies[index];
    }

    public void addEnemy(GameObject enemy) => enemies.Add(enemy);

    public void removeEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public void Update()
    {
        if (playerPosition != lastPlayerPosition)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyTree>().PositionUpdate(playerPosition);
            }
            lastPlayerPosition = playerPosition;
        }
    }

    public List<GameObject> getEnemies()
    {
        return enemies;
    }
}
