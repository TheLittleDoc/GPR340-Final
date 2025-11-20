using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTrees;
using UnityEngine;

[Serializable]
public class BehaviorTree : MonoBehaviour
{
    public Node rootNode;

    private Node.NodeState treestate;
    // Start is called before the first frame update

    protected virtual bool Ready() { return true; }

    public virtual void SetCorners(List<Vector3> corners)
    {
        
    }
}