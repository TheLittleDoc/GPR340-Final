using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviorTrees
{
    [System.Serializable] 
    public abstract class Node
    {
        public enum NodeType
        {
            Root,
            Sequence,
            Selector,
            Action,
            Condition
        }

        public enum NodeState
        {
            NotReady,
            Ready,
            Running,
            Success,
            Failure
        }
        [SerializeField]
        protected NodeType nodeType;
        
        [SerializeField]
        protected NodeState nodeState = NodeState.NotReady;
    
        // Start is called before the first frame update
        protected Node() {}

        public delegate NodeState NodeReturn();
        public abstract NodeState Evaluate();
        public abstract bool Ready();
        public NodeState GetState() { return nodeState; }
    
    }
}
