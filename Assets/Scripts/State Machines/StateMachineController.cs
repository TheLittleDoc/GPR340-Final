using UnityEngine;

namespace StateMachines
{
    public class StateMachineController : MonoBehaviour
    {
        public State initialState;
        public State currentState;

        void State()
        {
            currentState = initialState;
            initialState.OnStateEnter(this);
        }

        private void CheckTransitions()
        {
            foreach (Transition transition in currentState.transitions)
            {
                
            }
        }
        
        
        
    }
}