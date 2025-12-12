using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private UIManager ui;
    public Blackboard blackboard;
    // Start is called before the first frame update
    void Awake()
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
        if (blackboard.getEnemies().Count == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ui.loadScene("YouWin");
        }
        
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
