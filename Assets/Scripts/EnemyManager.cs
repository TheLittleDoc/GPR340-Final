using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    //[SerializeField] private List<NavMeshAgent> agents;
    [SerializeField] private List<Agent> agents;
    [SerializeField] private List<Vector3> areaCorners; 

    private bool paceCompleted = true;
    
    // Update is called once per frame
    void Update()
    {
        
    }


}
