using UnityEngine;

namespace StateMachines
{
    [CreateAssetMenu(fileName = "Condition", menuName = "GPR340/Condition")]
    public class Condition : ScriptableObject
    {
        public virtual bool CheckCondition(StateMachineController controller) { return false; }
    }
}