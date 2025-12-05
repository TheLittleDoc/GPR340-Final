using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    private Vector3 playerPosition;
    private static Blackboard blackboard;

    private float health;

    private void Awake()
    {
        blackboard = this;
    }

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
}
