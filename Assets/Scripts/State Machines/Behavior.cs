using System;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachines
{
    public class Behavior : ScriptableObject
    {
        public virtual void Execute(StateMachineController controller)
        {
            return;
        }

    }
    [CreateAssetMenu(fileName = "Behavior", menuName = "GPR340/Behaviors/BehaviorTree")]
    public class PacingBehavior : Behavior
    {
        public BehaviorTree behaviorTree;
        public override void Execute(StateMachineController controller)
        {
            behaviorTree.enabled = true;

        }
    }
}