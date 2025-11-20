using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct Agent
{
    public NavMeshAgent agent;
    public BehaviorTree behaviorTree;

    public void SetPacePoints(List<Vector3> corners)
    {
        EnemyTree enemyTree = behaviorTree as EnemyTree;
        enemyTree.SetCorners(corners);
    }
}