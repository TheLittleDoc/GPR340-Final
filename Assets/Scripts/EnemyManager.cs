using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private List<NavMeshAgent> agents;
    [SerializeField] private List<Vector3> areaCorners; 

    private bool paceCompleted = true;
    
    // Update is called once per frame
    void Update()
    {
        if (paceCompleted)
        {
            StartCoroutine(pace(agents[0], areaCorners));
        }
    }

    private IEnumerator pace(NavMeshAgent agent, List<Vector3> corners)
    {
        paceCompleted = false;
        for (int i = 0; i < corners.Count; i++)
        {
            agent.SetDestination(corners[i]);

            while (agent.pathPending || agent.remainingDistance > 2.0f)
            {
                yield return null;
            }
            
            Debug.Log("reached corner: " + i + 1);
        }
        
        paceCompleted = true;
    }
}
