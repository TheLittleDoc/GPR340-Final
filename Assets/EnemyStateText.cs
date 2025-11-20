using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStateText : MonoBehaviour
{
    [SerializeField] private EnemyTree enemyTree;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyTree.GetState())
        {
            case EnemyTree.EnemyState.Patrolling:
                GetComponent<TMP_Text>().text = "Enemy State: Patrolling";
                break;
            case EnemyTree.EnemyState.Spotted:
                GetComponent<TMP_Text>().text = "Enemy State: Spotted";
                break;
            case EnemyTree.EnemyState.Searching:
                GetComponent<TMP_Text>().text = "Enemy State: Searching";
                break;
            default:
                GetComponent<TMP_Text>().text = "Enemy State: ";
                break;         
        }
        
    }
}
