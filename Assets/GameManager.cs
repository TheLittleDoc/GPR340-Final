using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Blackboard blackboard;
    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null)
            gameManager = this;
        if(gameManager != this)
            Destroy(gameObject);
        blackboard = Blackboard.instance;
    }

    // Update is called once per frame
    void Update()
    {
        blackboard.Update();
        
    }

    public void ReportPosition(Vector3 playerPosition)
    {
        if(blackboard != null)
            blackboard.setPlayerPosition(playerPosition);
        
    }

    public Blackboard getBlackboard()
    {
        return blackboard;
    }
    
    
    
}
