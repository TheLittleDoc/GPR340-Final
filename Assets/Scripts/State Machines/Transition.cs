using UnityEngine;

namespace StateMachines
{
    [CreateAssetMenu(fileName = "Transition", menuName = "GPR340/Transition")]
    public class Transition : ScriptableObject
    {
        public Condition condition;

        public State targetState;
            
            
    }
}