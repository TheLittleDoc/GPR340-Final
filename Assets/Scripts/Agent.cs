using System;
using UnityEngine.AI;

[Serializable]
public struct Agent
{
    public NavMeshAgent agent;
    public BehaviorTree behaviorTree;
}