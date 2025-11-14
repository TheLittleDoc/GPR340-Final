using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTrees;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class EnemyTree : BehaviorTree
{
    NavMeshAgent agent;
    [SerializeField] private double paceDelay = 3.0;
    private double waitingTime = 3.0;
    private ActionNode paceNode;
    private ActionNode waitNode;
    private int currentCorner = 0;
    [SerializeField] private List<Vector3> areaCorners; 


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ActionNode.ActionNodeDelegate pace = Pace;
        paceNode = new ActionNode(pace);
        ActionNode.ActionNodeDelegate wait = WaitToPace;
        waitNode = new ActionNode(wait);
        List<Node> nodes = new List<Node>();
        nodes.Add(paceNode);
        nodes.Add(waitNode);
        rootNode = new Sequence(nodes);
        Ready();
    }

    private Node.NodeState WaitToPace()
    {
        if(waitingTime > 0.0)
        {
            waitingTime -= Time.deltaTime;
            return Node.NodeState.Running;
        }

        waitingTime = paceDelay;
        return Node.NodeState.Success;
        
        
    }

    // Start is called before the first frame update
    public void FixedUpdate()
    {
       rootNode.Evaluate();
        switch (rootNode.GetState())
        {
            case Node.NodeState.Success:
                Debug.Log("Success");
                Ready();
                break;
            case Node.NodeState.Failure:
                Debug.Log("Failure");
                break;
            case Node.NodeState.Running:
                Debug.Log("Running");
                break;
            default:
                Debug.Log("Unknown state");
                break;
        }
        Debug.Log(waitingTime);
    }

    protected override bool Ready()
    {
        if(!rootNode.Ready())
            throw new System.Exception("Root node not ready");
        Debug.Log("Ready");
        waitingTime = paceDelay;
        currentCorner %= 4;
        return true;
    }

    public Node.NodeState Pace()
    {
        // return Success if agent reaches corner, Failure if player detected within radius
        if (paceNode.GetState() == Node.NodeState.Success)
        {
            Debug.Log("Paced");
            return Node.NodeState.Success;
        }
        if (paceNode.GetState() == Node.NodeState.Ready)
        {
            agent.SetDestination(areaCorners[currentCorner]);
        }

        if (agent.pathPending || agent.remainingDistance > 2.0f)
        {
            
            return Node.NodeState.Running;
        }

        Debug.Log("reached corner: " + currentCorner + 1);
        
            currentCorner++;
        return Node.NodeState.Success;

    }
    
    
    private IEnumerator pace(NavMeshAgent agent, List<Vector3> corners)
    {
        for (int i = 0; i < corners.Count; i++)
        {
            agent.SetDestination(corners[i]);

            while (agent.pathPending || agent.remainingDistance > 2.0f)
            {
                yield return null;
            }
            
            Debug.Log("reached corner: " + i + 1);
        }
        
    }
}
