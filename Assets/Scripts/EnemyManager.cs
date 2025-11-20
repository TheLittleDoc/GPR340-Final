using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class EnemyManager : MonoBehaviour
{

    //[SerializeField] private List<NavMeshAgent> agents;
    [SerializeField] private List<Agent> agents;
    [SerializeField] private List<Vector3> areaCorners; 
    private bool paceCompleted = true;
    
    // Update is called once per frame
    void Start()
    {
        foreach (Agent agent in agents)
        {
            // Randomly generate four points within a defined area for the agent to pace between
            List<Vector3> corners = new List<Vector3>();
            float x0 = Mathf.Min(areaCorners[0].x, areaCorners[1].x);
            float z0 = Mathf.Min(areaCorners[0].z, areaCorners[1].z);
            float x1 = Mathf.Max(areaCorners[0].x, areaCorners[1].x);
            float z1 = Mathf.Max(areaCorners[0].z, areaCorners[1].z);
            
            float x = UnityEngine.Random.Range(x0, 0);
            float z = UnityEngine.Random.Range(z0, 0);
            corners.Add(new Vector3(x, 1, z));
            x = UnityEngine.Random.Range(0, x1);
            z = UnityEngine.Random.Range(z0, 0);
            corners.Add(new Vector3(x, 1, z));
            x = UnityEngine.Random.Range(0, x1);
            z = UnityEngine.Random.Range(0, z1);
            corners.Add(new Vector3(x, 1, z));
            x = UnityEngine.Random.Range(x0, 0);
            z = UnityEngine.Random.Range(0, z1);
            corners.Add(new Vector3(x, 1, z));
            //sort corners clockwise to form a square
            
            corners.Sort((a, b) =>
            {
                float angleA = Mathf.Atan2(a.z - (z0 + z1) / 2, a.x - (x0 + x1) / 2);
                float angleB = Mathf.Atan2(b.z - (z0 + z1) / 2, b.x - (x0 + x1) / 2);
                return angleA.CompareTo(angleB);
            });
            
            agent.SetPacePoints(corners);
            
            
            
        }
    }


}
