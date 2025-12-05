using System.Collections.Generic;
using UnityEngine;

namespace StateMachines
{
    [CreateAssetMenu(fileName = "State", menuName = "GPR340/State")]
        public class State : ScriptableObject
        {
            public List<Transition> transitions = new List<Transition>();
            public BehaviorTree behaviorTree;
            public virtual void OnStateEnter(StateMachineController controller)
            {
                
            }

            public virtual void StateUpdate(StateMachineController controller)
            {
                return;
            }

            public virtual void OnStateExit(StateMachineController controller)
            {
                return;
            }
        }
    
}
