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
    [SerializeField] private double detectionRadius = 5.0;
    private double waitingTime = 3.0;
    private ActionNode paceNode;
    private ActionNode waitNode;
    private ActionNode searchNode;
    private int currentCorner = 0;
    private Sequence paceSequence;
    private Sequence foundSequence;
    [SerializeField] private List<Vector3> areaCorners; 
    //GameObject player;

    private bool ccw = false;

    public void Start()
    {
        
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            Debug.Log("Counterclockwise");
            ccw = true;
        }
        agent = GetComponent<NavMeshAgent>();
        ActionNode.ActionNodeDelegate pace = Pace;
        paceNode = new ActionNode(pace);
        ActionNode.ActionNodeDelegate wait = WaitToPace;
        waitNode = new ActionNode(wait);
        ActionNode.ActionNodeDelegate search = CheckForPlayer;
        searchNode = new ActionNode(search);
        List<Node> nodes = new List<Node>();
        nodes.Add(paceNode);
        nodes.Add(waitNode);
        nodes.Add(searchNode);
        paceSequence = new Sequence(nodes);
        rootNode = paceSequence;
        
        Ready();
        //player = GameObject.FindGameObjectWithTag("Player");
    }



    // Start is called before the first frame update
    public void FixedUpdate()
    {
       rootNode.Evaluate();
        switch (rootNode.GetState())
        {
            case Node.NodeState.Success:
                //Debug.Log("Success");
                Ready();
                break;
            case Node.NodeState.Failure:
                //Debug.Log("Failure");
                MeshRenderer renderer = GetComponent<MeshRenderer>();
                renderer.material.color = Color.red;
                break;
            case Node.NodeState.Running:
                //Debug.Log("Running");
                break;
            default:
                //Debug.Log("Unknown state");
                break;
        }
        //Debug.Log(waitingTime);
    }

    protected override bool Ready()
    {
        if(!rootNode.Ready())
            throw new System.Exception("Root node not ready");
        //Debug.Log("Ready");
        waitingTime = paceDelay;
        currentCorner %= 4;
        return true;
    }

    public Node.NodeState Pace()
    {
        // return Success if agent reaches corner, Failure if player detected within radius
        if (paceNode.GetState() == Node.NodeState.Success)
        {
            //Debug.Log("Paced");
            return Node.NodeState.Success;
        }
        if (paceNode.GetState() == Node.NodeState.Ready)
        {
            agent.SetDestination(areaCorners[currentCorner]);
        }
        double distance = Vector3.Distance(Blackboard.instance.getPlayerPosition(), transform.position) * 1.25;
        if (agent.pathPending || agent.remainingDistance > 1.0f)
        {
            
            if(distance < detectionRadius)
            {
                Debug.Log(distance);
                agent.SetDestination(transform.position + (agent.velocity.normalized));
                
            }
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material.color = Color.green;
            return Node.NodeState.Running;
        }
        
        

        //Debug.Log("reached corner: " + currentCorner + 1);
        if(distance > detectionRadius)
        {
            currentCorner = ccw ? (currentCorner + 1) % 4 : (currentCorner + 3) % 4;
                
        }
        return Node.NodeState.Success;

    }
    
    
    private Node.NodeState WaitToPace()
    {
        if(waitNode.GetState() == Node.NodeState.Ready)
        {
            Debug.Log("WaitToPace");
            StartCoroutine(LookLeftAndRight());
        }
        
        if(waitingTime > 0.0)
        {
            waitingTime -= Time.deltaTime;
            return Node.NodeState.Running;
        }

        waitingTime = paceDelay;
        return Node.NodeState.Success;
        
        
    }

    private Node.NodeState CheckForPlayer()
    {   
        double distance = Vector3.Distance(Blackboard.instance.getPlayerPosition(), transform.position);
        if(distance < detectionRadius)
            return Node.NodeState.Failure;
        return Node.NodeState.Success;
    }

    private IEnumerator LookLeftAndRight()
    {
        // add a left and right rotation over time
        Quaternion initialRotation = transform.rotation;
        Quaternion leftRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y - 30f, initialRotation.eulerAngles.z);
        Quaternion rightRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + 30f, initialRotation.eulerAngles.z);

        float t = 0.0f;
        float thirdTime = (float)waitingTime / 3.0f;
        float timeElapsed = 0.0f;
        while (timeElapsed < thirdTime)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, leftRotation, t);
            timeElapsed += Time.deltaTime;
            t = timeElapsed / thirdTime;
            yield return null;
            
        }

        t = 0.0f;
        timeElapsed = 0.0f;
        while (timeElapsed < thirdTime)
        {
            transform.rotation = Quaternion.Slerp(leftRotation, rightRotation, t);
            timeElapsed += Time.deltaTime;
            t = timeElapsed / thirdTime;
            yield return null;

        }

        t = 0.0f;
        timeElapsed = 0.0f;
        while (timeElapsed < thirdTime)
        {
            transform.rotation = Quaternion.Slerp(rightRotation, initialRotation, t);
            timeElapsed += Time.deltaTime;
            t = timeElapsed / thirdTime;
            yield return null;

        }
        yield return null;
    }
    
    public enum EnemyState
    {
        Patrolling,
        Chasing,
        Searching,
        Attacking,
        Spotted
    }

    public EnemyState GetState()
    {
        if (paceNode.GetState() == Node.NodeState.Running)
            return EnemyState.Patrolling;
        if (searchNode.GetState() == Node.NodeState.Failure)
            return EnemyState.Spotted;
        if (waitNode.GetState() == Node.NodeState.Running)
            return EnemyState.Searching;
        return EnemyState.Patrolling;
    }
    
    public void SetCorners(List<Vector3> corners)
    {
        areaCorners = corners;
    }
}
