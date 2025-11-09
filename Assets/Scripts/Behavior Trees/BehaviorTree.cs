using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTrees;
using UnityEngine;

[System.Serializable]
public class BehaviorTree : MonoBehaviour
{
    [SerializeField] Node rootNode;

    private Node.NodeState treestate;
    [SerializeField]
    double timeRemaining = 3;
    // Start is called before the first frame update
    void Start()
    {
        ActionNode.ActionNodeDelegate action1 = Action1;
        ActionNode.ActionNodeDelegate action2 = Action2;
        ActionNode node1 = new ActionNode(action2);
        ActionNode node2 = new ActionNode(action1);
        List<Node> actionNodes = new List<Node>();
        actionNodes.Add(node1);
        actionNodes.Add(node2);
        rootNode = new Selector(actionNodes);

        rootNode.Ready();


    }

    private Node.NodeState Action1()
    {
        if (gameObject.name == "Tree")
            return Node.NodeState.Success;
        return Node.NodeState.Failure;
    }

    private Node.NodeState Action2()
    {
        if(timeRemaining <= 0)
            return Node.NodeState.Failure;
        timeRemaining -= Time.deltaTime;
        return Node.NodeState.Running;
        
    }

// Update is called once per frame
    void Update()
    {
        Node.NodeState treestate = rootNode.Evaluate();
        switch (rootNode.GetState())
        {
            case Node.NodeState.Success:
                Debug.Log("Success");
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
        
    }
}
